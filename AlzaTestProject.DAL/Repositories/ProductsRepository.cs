using AlzaTestProject.DAL.Contextes;
using AlzaTestProject.DAL.Entities;
using AlzaTestProject.DAL.Extensions;
using AlzaTestProject.DAL.Specifications;
using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Repositories
{
	internal class ProductsRepository : IRepository<Product>
	{
		private readonly AppDbContext _dbContext;

		public ProductsRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<int> Count(ISpecification specification, CancellationToken cancellationToken)
		{
			if (specification is IOrmSpecification<ProductEntity> ormSpec)
			{
				return await _dbContext.Products
					.Where(ormSpec.Criteria)
					.AsNoTracking()
					.CountAsync(cancellationToken);
			}

			throw new InvalidOperationException("Wrong type of specification.");
		}

		public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default)
		{
			return (await _dbContext.Products.AsNoTracking()
				.ToListAsync(cancellationToken)).Select(p => p.MapToDom());
		}

		public async Task<IEnumerable<Product>> GetAll(ISpecification specification,
			CancellationToken cancellationToken = default)
		{
			if (specification is IOrmSpecification<ProductEntity> ormSpec)
			{
				var products = await _dbContext.Products
					.Where(ormSpec.Criteria)
					.AsNoTracking()
					.ToListAsync(cancellationToken);

				return products.Select(p => p.MapToDom());
			}

			throw new InvalidOperationException("Wrong type of specification.");
		}

		public async Task<Product?> GetById(int id, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Products.FindAsync([id], cancellationToken);
			if (entity is null)
				return null;

			return entity.MapToDom();
		}

		public async Task<bool> Exists(ISpecification specification,
			CancellationToken cancellationToken = default)
		{
			if (specification is IOrmSpecification<ProductEntity> ormSpec)
			{
				return await _dbContext.Products.AnyAsync(ormSpec.Criteria, cancellationToken);
			}

			throw new InvalidOperationException("Wrong type of specification.");
		}

		public Product Add(Product item)
		{
			var entity = item.MapToDal();
			_dbContext.Products.Add(entity);

			return entity.MapToDom();
		}

		public async Task<Product> Update(Product item, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Products.FindAsync([item.Id], cancellationToken);

			if(entity is null)
				throw new KeyNotFoundException(item.Id.ToString());

			item.MapToDal(entity);

			return entity.MapToDom();
		}

		public void Delete(Product item)
		{
			_dbContext.Products.Remove(item.MapToDal());
		}
	}
}
