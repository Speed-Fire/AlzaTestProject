using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Requests;
using AlzaTestProject.Services.Workers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Services.Extensions
{
	public static class DIExtensions
	{
		public static IServiceCollection AddAlzaTestProjectServices(this IServiceCollection services)
		{
			services
				.AddTransient<IProductService, ProductService>()
				.AddSingleton<IAsyncQueue<UpdateStockRequest>, StockUpdateQueueInMemory>();

			return services;
		}

		public static IServiceCollection AddStockUpdateWorker(this IServiceCollection services)
		{
			services
				.AddHostedService<StockUpdateWorker>();

			return services;
		}
	}
}
