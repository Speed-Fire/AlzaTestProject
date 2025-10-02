using AlzaTestProject.DAL.Contextes;
using AlzaTestProject.Domain.Abstract;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _dbContext;
		private readonly ConcurrentDictionary<Type, object> _repositories = [];
		private readonly IServiceProvider _serviceProvider;

		private IDbContextTransaction? _transaction;

		public UnitOfWork(AppDbContext dbContext, IServiceProvider serviceProvider)
		{
			_dbContext = dbContext;
			_serviceProvider = serviceProvider;
		}

		public IRepository<TModel> GetRepository<TModel>()
		{
			var type = typeof(TModel);

			var repository = _repositories.GetOrAdd(type, t =>
				_serviceProvider.GetRequiredService<IRepository<TModel>>());

			return (IRepository<TModel>)repository;
		}

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return _dbContext.SaveChangesAsync(cancellationToken);
		}

		public Task BeginTransaction(CancellationToken cancellationToken = default)
		{
			if (_transaction is null)
				return _dbContext.Database.BeginTransactionAsync(cancellationToken);

			return Task.CompletedTask;
		}

		public async Task CommitTransaction(CancellationToken cancellationToken = default)
		{
			if (_transaction is null)
				throw new InvalidOperationException("Trying to commit not existing transaction.");

			await _dbContext.SaveChangesAsync(cancellationToken);
			await _transaction.CommitAsync(cancellationToken);
			await _transaction.DisposeAsync();

			_transaction = null;
		}

		public async Task RollbackTransaction(CancellationToken cancellationToken = default)
		{
			if (_transaction is null)
				throw new InvalidOperationException("Trying to rollback not existing transaction.");

			await _transaction.RollbackAsync(cancellationToken);
			await _transaction.DisposeAsync();

			_transaction = null;
		}

		public void Dispose()
		{
			_transaction?.Dispose();
		}

		public ValueTask DisposeAsync()
		{
			if (_transaction is not null)
				return _transaction.DisposeAsync();

			return ValueTask.CompletedTask;
		}
	}
}
