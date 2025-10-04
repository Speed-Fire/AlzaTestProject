using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AlzaTestProject.Services
{
	internal class StockUpdateQueueInMemory : IAsyncQueue<UpdateStockRequest>
	{
		private readonly Channel<UpdateStockRequest> _channel
			= Channel.CreateUnbounded<UpdateStockRequest>();

		public async Task EnqueueAsync(UpdateStockRequest request, CancellationToken cancellationToken = default)
		{
			await _channel.Writer.WriteAsync(request, cancellationToken);
		}

		public async Task<UpdateStockRequest?> DequeueAsync(CancellationToken cancellationToken = default)
		{
			return await _channel.Reader.ReadAsync(cancellationToken);
		}

		public void Dispose()
		{
			
		}

		public ValueTask DisposeAsync()
		{
			return ValueTask.CompletedTask;
		}
	}
}
