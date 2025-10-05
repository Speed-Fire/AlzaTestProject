using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	/// <summary>
	/// Represents an asynchronous queue that allows enqueuing and dequeuing of requests of type <typeparamref name="TRequest"/>.
	/// </summary>
	/// <typeparam name="TRequest">The type of request stored in the queue.</typeparam>
	public interface IAsyncQueue<TRequest> : IDisposable, IAsyncDisposable
	{
		/// <summary>
		/// Adds a request to the queue asynchronously.
		/// </summary>
		/// <param name="request">The request to enqueue.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>A task that represents the asynchronous enqueue operation.</returns>
		Task EnqueueAsync(TRequest request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Removes and returns the next request from the queue asynchronously.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>
		/// A task that represents the asynchronous dequeue operation. 
		/// The result is the dequeued request, or <c>null</c> if the queue is empty.
		/// </returns>
		Task<TRequest?> DequeueAsync(CancellationToken cancellationToken = default);
	}
}
