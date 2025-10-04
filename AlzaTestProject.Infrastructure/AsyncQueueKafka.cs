using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Requests;
using AlzaTestProject.Services.Kafka.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlzaTestProject.Services.Kafka
{
	public class AsyncQueueKafka<TRequest> : IAsyncQueue<TRequest>
	{
		private readonly IProducer<Null, string> _producer;
		private readonly IConsumer<Null, string> _consumer;
		private readonly ILogger _logger;

		private readonly string _topic;

		public AsyncQueueKafka(
			ILogger logger,
			KafkaOptions kafkaOptions,
			KafkaQueueOptions queueOptions)
		{
			_logger = logger;

			_producer = MakeProducer(kafkaOptions);
			_consumer = MakeConsumer(kafkaOptions, queueOptions);

			_topic = queueOptions.Topic;
		}

		public async Task EnqueueAsync(TRequest request, CancellationToken cancellationToken = default)
		{
			var json = JsonSerializer.Serialize(request);
			try
			{
				var result = await _producer.ProduceAsync(
					_topic,
					new Message<Null, string> { Value = json },
					cancellationToken);

				_logger.LogInformation(
					"Enqueued message of type {RequestType} to topic {Topic}. Partition: {Partition}, Offset: {Offset}",
					typeof(TRequest).Name,
					_topic,
					result.Partition.Value,
					result.Offset.Value);
			}
			catch (ProduceException<Null, string> ex)
			{
				_logger.LogError(
					ex,
					"Error producing message of type {RequestType} to Kafka topic {Topic}",
					typeof(TRequest).Name,
					_topic);
				throw;
			}
		}

		public Task<TRequest?> DequeueAsync(CancellationToken cancellationToken = default)
		{
			try
			{
				var cr = _consumer.Consume(cancellationToken);
				if (cr?.Message?.Value == null)
				{
					_logger.LogWarning(
						"Dequeued null/empty message of type {RequestType} from topic {Topic}",
						typeof(TRequest).Name,
						_topic);
					return Task.FromResult<TRequest?>(default);
				}

				var obj = JsonSerializer.Deserialize<TRequest>(cr.Message.Value);

				_logger.LogInformation(
					"Dequeued message of type {RequestType} from topic {Topic}. Partition: {Partition}, Offset: {Offset}",
					typeof(TRequest).Name,
					_topic,
					cr.Partition.Value,
					cr.Offset.Value);

				return Task.FromResult<TRequest?>(obj);
			}
			catch (ConsumeException ex)
			{
				_logger.LogError(
					ex,
					"Kafka consume error while processing message of type {RequestType} from topic {Topic}",
					typeof(TRequest).Name,
					_topic);
				return Task.FromResult<TRequest?>(default);
			}
		}

		public void Dispose()
		{
			_producer.Dispose();
			_consumer.Close();
			_consumer.Dispose();
		}

		public ValueTask DisposeAsync()
		{
			Dispose();
			return ValueTask.CompletedTask;
		}

		private static IProducer<Null, string> MakeProducer(
			KafkaOptions kafkaOpts)
		{
			var producerConfig = new ProducerConfig
			{
				BootstrapServers = kafkaOpts.BootstrapServers,
			};

			if (!string.IsNullOrEmpty(kafkaOpts.Username) && !string.IsNullOrEmpty(kafkaOpts.Password))
			{
				producerConfig.SaslMechanism = 
					Enum.Parse<SaslMechanism>(kafkaOpts.SaslMechanism!, true);
				producerConfig.SecurityProtocol = 
					Enum.Parse<SecurityProtocol>(kafkaOpts.SecurityProtocol!, true);
				producerConfig.SaslUsername = kafkaOpts.Username;
				producerConfig.SaslPassword = kafkaOpts.Password;
			}

			var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
			return producer;
		}

		private static IConsumer<Null, string> MakeConsumer(
			KafkaOptions kafkaOpts,
			KafkaQueueOptions queueOpts)
		{
			var consumerConfig = new ConsumerConfig
			{
				BootstrapServers = kafkaOpts.BootstrapServers,
				GroupId = queueOpts.GroupId,
				AutoOffsetReset = AutoOffsetReset.Earliest
			};

			if (!string.IsNullOrEmpty(kafkaOpts.Username) && !string.IsNullOrEmpty(kafkaOpts.Password))
			{
				consumerConfig.SaslMechanism =
					Enum.Parse<SaslMechanism>(kafkaOpts.SaslMechanism!, true);
				consumerConfig.SecurityProtocol =
					Enum.Parse<SecurityProtocol>(kafkaOpts.SecurityProtocol!, true);
				consumerConfig.SaslUsername = kafkaOpts.Username;
				consumerConfig.SaslPassword = kafkaOpts.Password;
			}

			var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
			consumer.Subscribe(queueOpts.Topic);

			return consumer;
		}
	}
}
