using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Services.Kafka;
using AlzaTestProject.Services.Kafka.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Infrastructure.Extensions
{
	public static class DIExtensions
	{
		public static IServiceCollection AddKafka(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.Configure<KafkaOptions>(configuration.GetSection("Kafka:General"));

			return services;
		}

		public static IServiceCollection AddKafkaQueue<TRequest>(
			this IServiceCollection services,
			IConfiguration configuration,
			string queueName)
		{
			services.Configure<KafkaQueueOptions>(queueName,
				configuration.GetSection($"Kafka:Queues:{queueName}"));
			
			services.AddSingleton<IAsyncQueue<TRequest>>(provider =>
			{
				var logger = provider.GetRequiredService<ILogger<AsyncQueueKafka<TRequest>>>();
				var kafkaOptions = provider.GetRequiredService<IOptions<KafkaOptions>>();
				var queueOptions = provider.GetRequiredService<IOptionsMonitor<KafkaQueueOptions>>()
					.Get(queueName);

				return new AsyncQueueKafka<TRequest>(
					logger,
					kafkaOptions.Value,
					queueOptions);
			});

			return services;
		}

		public static IServiceCollection AddKeyedKafkaQueue<TRequest>(
			this IServiceCollection services,
			object serviceKey,
			IConfiguration configuration,
			string queueName)
		{
			services.Configure<KafkaQueueOptions>(queueName,
				configuration.GetSection($"Kafka:Queues:{queueName}"));

			services.AddKeyedSingleton<IAsyncQueue<TRequest>>(serviceKey, (provider, key) =>
			{
				var logger = provider.GetRequiredService<ILogger<AsyncQueueKafka<TRequest>>>();
				var kafkaOptions = provider.GetRequiredService<IOptions<KafkaOptions>>();
				var queueOptions = provider.GetRequiredService<IOptionsSnapshot<KafkaQueueOptions>>()
					.Get(queueName);

				return new AsyncQueueKafka<TRequest>(
					logger,
					kafkaOptions.Value,
					queueOptions);
			});

			return services;
		}
	}
}
