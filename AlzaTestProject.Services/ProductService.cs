using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Models;
using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Dtos;
using AlzaTestProject.Services.Extensions;
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

		public ProductService(IUnitOfWork uow, IProductSpecificationFactory productSpecificationFactory)
		{
			_uow = uow;
			_productsRepository = uow.GetRepository<Product>();
			_productSpecificationFactory = productSpecificationFactory;
		}

		public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return (await _productsRepository.GetAll()).Select(p => p.MapToDto());
		}

		public async Task<OneOf<ProductDto, NotFound>> GetByIdAsync(int id,
			CancellationToken cancellationToken = default)
		{
			var product = await _productsRepository.GetById(id, cancellationToken);
			if (product is null)
				return new NotFound();

			return product.MapToDto();
		}

		public async Task<OneOf<ProductDto, Error<string>>> CreateAsync(CreateProductDto createProductDto,
			CancellationToken cancellationToken = default)
		{
			try
			{
				if (await _productsRepository.Exists(_productSpecificationFactory.ExistsByNameSpecification(createProductDto.Name)))
					return new Error<string>("Product with the same name already exists.");

				var product = new Product(createProductDto.Name, createProductDto.ImageUrl);
				product = _productsRepository.Add(product);

				await _uow.SaveChangesAsync(cancellationToken);

				return product.MapToDto();
			}
			catch (ArgumentException ex)
			{
				return new Error<string>(ex.Message);
			}
		}

		public async Task<OneOf<ProductDto, NotFound, Error<string>>> UpdateStockAsync(int id, UpdateStockDto updateStockDto,
			CancellationToken cancellationToken = default)
		{
			try
			{
				var product = await _productsRepository.GetById(id);
				if (product is null)
					return new NotFound();

				product.UpdateStock(updateStockDto.NewStock);
				product = await _productsRepository.Update(product, cancellationToken);

				await _uow.SaveChangesAsync(cancellationToken);

				return product.MapToDto();
			}
			catch (ArgumentException ex)
			{
				return new Error<string>(ex.Message);
			}
		}
	}
}
