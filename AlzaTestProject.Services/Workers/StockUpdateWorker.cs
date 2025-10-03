using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Services.Workers
{
	public class StockUpdateWorker : BackgroundService
	{
		private readonly IAsyncQueue<UpdateStockRequest> _queue;
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly ILogger _logger;

		public StockUpdateWorker(
			IAsyncQueue<UpdateStockRequest> queue,
			IServiceScopeFactory scopeFactory,
			ILogger<StockUpdateWorker> logger)
		{
			_queue = queue;
			_scopeFactory = scopeFactory;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("StockUpdateWorker started.");

			while (!stoppingToken.IsCancellationRequested)
			{
				UpdateStockRequest? request = null;

				try
				{
					request = await _queue.DequeueAsync(stoppingToken);

					if (request is null)
						continue;

					using var scope = _scopeFactory.CreateScope();
					var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

					_logger.LogInformation("Processing stock update for ProductId={ProductId}, NewStock={NewStock}",
						request.ProductId, request.NewStock);

					await productService.UpdateStockAsync(
						request.ProductId,
						new() { NewStock = request.NewStock },
						stoppingToken);

					_logger.LogInformation("Successfully updated stock for ProductId={ProductId}", request.ProductId);
				}
				catch (OperationCanceledException)
				{
					_logger.LogInformation("StockUpdateWorker stopping due to cancellation.");
					break;
				}
				catch (Exception ex)
				{
					if (request != null)
					{
						_logger.LogError(ex, "Error updating stock for ProductId={ProductId}", request.ProductId);
					}
					else
					{
						_logger.LogError(ex, "Error in StockUpdateWorker loop");
					}
				}
			}

			_logger.LogInformation("StockUpdateWorker stopped.");
		}
	}
}
