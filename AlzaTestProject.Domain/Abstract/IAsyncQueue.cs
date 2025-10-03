using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	public interface IAsyncQueue<TRequest> : IDisposable, IAsyncDisposable
	{
		Task EnqueueAsync(TRequest request, CancellationToken cancellationToken = default);
		Task<TRequest?> DequeueAsync(CancellationToken cancellationToken = default);
	}
}
