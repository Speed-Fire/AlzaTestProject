using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	/// <summary>
	/// Represents a unit of work that coordinates the work of multiple repositories
	/// and ensures atomic operations within a transaction scope.
	/// </summary>
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{
		/// <summary>
		/// Retrieves a repository for the specified entity type.
		/// </summary>
		/// <typeparam name="TModel">The type of the entity for which the repository is requested.</typeparam>
		/// <returns>An instance of <see cref="IRepository{TModel}"/> for the specified entity type.</returns>
		IRepository<TModel> GetRepository<TModel>();

		/// <summary>
		/// Saves all pending changes to the underlying data store.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>The number of state entries written to the data store.</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Begins a new transaction.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task BeginTransaction(CancellationToken cancellationToken = default);

		/// <summary>
		/// Commits the current transaction, applying all changes atomically.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task CommitTransaction(CancellationToken cancellationToken = default);

		/// <summary>
		/// Rolls back the current transaction, discarding all changes made during the transaction.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task RollbackTransaction(CancellationToken cancellationToken = default);
	}
}
