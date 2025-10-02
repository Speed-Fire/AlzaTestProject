using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	public interface IUnitOfWork : IDisposable, IAsyncDisposable
	{
		IRepository<TModel> GetRepository<TModel>();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		Task BeginTransaction(CancellationToken cancellationToken = default);
		Task CommitTransaction(CancellationToken cancellationToken = default);
		Task RollbackTransaction(CancellationToken cancellationToken = default);
	}
}
