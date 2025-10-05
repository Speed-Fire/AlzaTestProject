using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Models;
using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Dtos;
using AlzaTestProject.Services.Extensions;
using AlzaTestProject.Services.Misc;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AlzaTestProject.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _uow;
		private readonly IRepository<Product> _productsRepository;
		private readonly IProductSpecificationFactory _productSpecificationFactory;
		private readonly ILogger _logger;

		public ProductService(
			IUnitOfWork uow,
			IProductSpecificationFactory productSpecificationFactory,
			ILogger<ProductService> logger)
		{
			_uow = uow;
			_productsRepository = uow.GetRepository<Product>();
			_productSpecificationFactory = productSpecificationFactory;
			_logger = logger;
		}

		public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Fetching all products");
			var products = await _productsRepository.GetAll(cancellationToken);
			_logger.LogInformation("Fetched {Count} products", products.Count());
			return products.Select(p => p.MapToDto());
		}

		public async Task<PagedResult<ProductDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			var totalItems = await _productsRepository.Count(
				_productSpecificationFactory
					.NoFilterSpecification(), cancellationToken);
			var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

			var result = new PagedResult<ProductDto>()
			{
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalItems = totalItems,
				TotalPages = totalPages
			};

			if (pageNumber > totalPages || pageNumber < 1)
			{
				return result;
			}

			var products = await _productsRepository.GetAll(
				_productSpecificationFactory
					.GetAllPagedSpecification(pageNumber, pageSize), cancellationToken);

			result.Items = products.Select(p => p.MapToDto());
			return result;
		}

		public async Task<OneOf<ProductDto, NotFound>> GetByIdAsync(int id,
			CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Fetching product by id {ProductId}", id);

			var product = await _productsRepository.GetById(id, cancellationToken);
			if (product is null)
			{
				_logger.LogWarning("Product with id {ProductId} not found", id);
				return new NotFound();
			}

			_logger.LogInformation("Product {ProductId} found", id);
			return product.MapToDto();
		}

		public async Task<OneOf<ProductDto, Error<string>>> CreateAsync(CreateProductDto createProductDto,
			CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Creating product {ProductName}", createProductDto.Name);

			try
			{
				await _uow.BeginTransaction(cancellationToken);

				if (await _productsRepository
					.Exists(_productSpecificationFactory
						.ExistsByNameSpecification(createProductDto.Name), cancellationToken))
				{
					_logger.LogWarning("Product creation failed. Product with name {ProductName} already exists", createProductDto.Name);
					return new Error<string>("Product with the same name already exists.");
				}

				var product = new Product(createProductDto.Name, createProductDto.ImageUrl);
				product = _productsRepository.Add(product);

				await _uow.SaveChangesAsync(cancellationToken);

				await _uow.CommitTransaction(cancellationToken);

				_logger.LogInformation("Product {ProductId} created successfully", product.Id);
				return product.MapToDto();
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, "Error creating product {ProductName}", createProductDto.Name);

				await _uow.RollbackTransaction(cancellationToken);
				
				return new Error<string>(ex.Message);
			}
			catch(Exception)
			{
				await _uow.RollbackTransaction(cancellationToken);
				throw;
			}
		}

		public async Task<OneOf<ProductDto, NotFound, Error<string>>> UpdateStockAsync(int id, UpdateStockDto updateStockDto,
			CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Updating stock for product {ProductId} to {NewStock}", id, updateStockDto.NewStock);

			try
			{
				await _uow.BeginTransaction(cancellationToken);

				var product = await _productsRepository.GetById(id, cancellationToken);
				if (product is null)
				{
					_logger.LogWarning("Product with id {ProductId} not found", id);
					return new NotFound();
				}

				product.UpdateStock(updateStockDto.NewStock);
				product = await _productsRepository.Update(product, cancellationToken);

				await _uow.SaveChangesAsync(cancellationToken);

				await _uow.CommitTransaction(cancellationToken);

				_logger.LogInformation("Stock for product {ProductId} updated successfully to {NewStock}", id, updateStockDto.NewStock);
				return product.MapToDto();
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, "Error updating stock for product {ProductId}", id);

				await _uow.RollbackTransaction(cancellationToken);

				return new Error<string>(ex.Message);
			}
			catch (Exception)
			{
				await _uow.RollbackTransaction(cancellationToken);
				throw;
			}
		}
	}
}
