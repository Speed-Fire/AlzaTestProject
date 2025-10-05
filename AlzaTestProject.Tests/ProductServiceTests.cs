using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Models;
using AlzaTestProject.Services;
using AlzaTestProject.Services.Dtos;
using Microsoft.Extensions.Logging;
using Moq;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Tests
{
	public class ProductServiceTests
	{
		private readonly Mock<IUnitOfWork> _uowMock;
		private readonly Mock<IRepository<Product>> _repoMock;
		private readonly Mock<IProductSpecificationFactory> _specFactoryMock;
		private readonly Mock<ILogger<ProductService>> _loggerMock;
		private readonly ProductService _service;

		public ProductServiceTests()
		{
			_uowMock = new Mock<IUnitOfWork>();
			_repoMock = new Mock<IRepository<Product>>();
			_specFactoryMock = new Mock<IProductSpecificationFactory>();
			_loggerMock = new Mock<ILogger<ProductService>>();

			_uowMock.Setup(u => u.GetRepository<Product>()).Returns(_repoMock.Object);

			_service = new ProductService(_uowMock.Object, _specFactoryMock.Object, _loggerMock.Object);
		}

		[Fact]
		public async Task GetAllAsync_ReturnsAllProducts()
		{
			// Arrange
			var products = new List<Product>
			{
				new Product(1, "Prod1", null, "http://image1", 10, 5),
				new Product(2, "Prod2", null, "http://image2", 20, 3)
			};

			_repoMock.Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
					 .ReturnsAsync(products);

			// Act
			var result = await _service.GetAllAsync();

			// Assert
			Assert.Equal(2, result.Count());
			Assert.Contains(result, p => p.Id == 1 && p.Name == "Prod1");
		}

		[Fact]
		public async Task GetByIdAsync_ProductExists_ReturnsProduct()
		{
			// Arrange
			var product = new Product(1, "Prod1", null, "http://image1", 10, 5);
			_repoMock.Setup(r => r.GetById(1, It.IsAny<CancellationToken>()))
					 .ReturnsAsync(product);

			// Act
			var result = await _service.GetByIdAsync(1);

			// Assert
			Assert.IsType<ProductDto>(result.AsT0);
			Assert.Equal("Prod1", result.AsT0.Name);
		}

		[Fact]
		public async Task GetByIdAsync_ProductDoesNotExist_ReturnsNotFound()
		{
			// Arrange
			_repoMock.Setup(r => r.GetById(1, It.IsAny<CancellationToken>()))
					 .ReturnsAsync((Product?)null);

			// Act
			var result = await _service.GetByIdAsync(1);

			// Assert
			Assert.IsType<NotFound>(result.AsT1);
		}

		[Fact]
		public async Task CreateAsync_ProductWithSameNameExists_ReturnsError()
		{
			// Arrange
			var dto = new CreateProductDto { Name = "Prod1", ImageUrl = "http://img" };
			_specFactoryMock.Setup(f => f.ExistsByNameSpecification(dto.Name))
							.Returns(Mock.Of<ISpecification>());
			_repoMock.Setup(r => r.Exists(It.IsAny<ISpecification>(), It.IsAny<CancellationToken>()))
					 .ReturnsAsync(true);

			// Act
			var result = await _service.CreateAsync(dto);

			// Assert
			Assert.IsType<Error<string>>(result.AsT1);
		}

		[Fact]
		public async Task CreateAsync_ValidProduct_ReturnsCreatedProduct()
		{
			// Arrange
			var dto = new CreateProductDto { Name = "Prod1", ImageUrl = "http://img" };
			_specFactoryMock.Setup(f => f.ExistsByNameSpecification(dto.Name))
							.Returns(Mock.Of<ISpecification>());
			_repoMock.Setup(r => r.Exists(It.IsAny<ISpecification>(), It.IsAny<CancellationToken>()))
					 .ReturnsAsync(false);
			_repoMock.Setup(r => r.Add(It.IsAny<Product>()))
					 .Returns<Product>(p => { p.Id = 1; return p; });
			_uowMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			// Act
			var result = await _service.CreateAsync(dto);

			// Assert
			Assert.IsType<ProductDto>(result.AsT0);
			Assert.Equal(1, result.AsT0.Id);
		}

		[Fact]
		public async Task CreateAsync_InalidProduct_ReturnsError()
		{
			// Arrange
			var dto = new CreateProductDto { Name = "Prod1", ImageUrl = "not a url" };
			_specFactoryMock.Setup(f => f.ExistsByNameSpecification(dto.Name))
							.Returns(Mock.Of<ISpecification>());
			_repoMock.Setup(r => r.Exists(It.IsAny<ISpecification>(), It.IsAny<CancellationToken>()))
					 .ReturnsAsync(false);
			_repoMock.Setup(r => r.Add(It.IsAny<Product>()))
					 .Returns<Product>(p => { p.Id = 1; return p; });
			_uowMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			// Act
			var result = await _service.CreateAsync(dto);

			// Assert
			Assert.IsType<Error<string>>(result.AsT1);
		}

		[Fact]
		public async Task UpdateStockAsync_ProductDoesNotExist_ReturnsNotFound()
		{
			// Arrange
			_repoMock.Setup(r => r.GetById(1, It.IsAny<CancellationToken>()))
					 .ReturnsAsync((Product?)null);

			// Act
			var result = await _service.UpdateStockAsync(1, new UpdateStockDto { NewStock = 10 });

			// Assert
			Assert.IsType<NotFound>(result.AsT1);
		}

		[Fact]
		public async Task UpdateStockAsync_ValidProduct_UpdatesStock()
		{
			// Arrange
			var product = new Product(1, "Prod1", null, "http://image1", 10, 5);
			_repoMock.Setup(r => r.GetById(1, It.IsAny<CancellationToken>()))
					 .ReturnsAsync(product);
			_repoMock.Setup(r => r.Update(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
					 .ReturnsAsync((Product p, CancellationToken _) => p);
			_uowMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			// Act
			var result = await _service.UpdateStockAsync(1, new UpdateStockDto { NewStock = 15 });

			// Assert
			Assert.IsType<ProductDto>(result.AsT0);
			Assert.Equal(15, result.AsT0.Stock);
		}

		[Fact]
		public async Task GetPagedAsync_ReturnsCorrectPagedResult()
		{
			// Arrange
			int pageNumber = 1;
			int pageSize = 2;
			var products = new List<Product>
		{
			new Product("A", "http://example.com/1") { Id = 1 },
			new Product("B", "http://example.com/2") { Id = 2 },
			new Product("C", "http://example.com/32") { Id = 3 }
		};

			_repoMock
				.Setup(r => r.Count(It.IsAny<ISpecification>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(products.Count);

			_specFactoryMock
				.Setup(f => f.NoFilterSpecification())
				.Returns(new Mock<ISpecification>().Object);

			_specFactoryMock
				.Setup(f => f.GetAllPagedSpecification(pageNumber, pageSize))
				.Returns(new Mock<ISpecification>().Object);

			_repoMock
				.Setup(r => r.GetAll(It.IsAny<ISpecification>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(products.Take(pageSize).ToList());

			// Act
			var result = await _service.GetPagedAsync(pageNumber, pageSize);

			// Assert
			Assert.Equal(pageNumber, result.PageNumber);
			Assert.Equal(pageSize, result.PageSize);
			Assert.Equal(products.Count, result.TotalItems);
			Assert.Equal((int)Math.Ceiling(products.Count / (double)pageSize), result.TotalPages);
			Assert.Equal(pageSize, result.Items.Count());
			Assert.Contains(result.Items, p => p.Id == 1);
			Assert.Contains(result.Items, p => p.Id == 2);
		}

		[Fact]
		public async Task GetPagedAsync_PageNumberExceedsTotalPages_ReturnsEmptyItems()
		{
			// Arrange
			int pageNumber = 10;
			int pageSize = 2;

			_repoMock
				.Setup(r => r.Count(It.IsAny<ISpecification>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(3); // всего 3 элемента, всего 2 страницы

			_specFactoryMock
				.Setup(f => f.NoFilterSpecification())
				.Returns(new Mock<ISpecification>().Object);

			// Act
			var result = await _service.GetPagedAsync(pageNumber, pageSize);

			// Assert
			Assert.Equal(pageNumber, result.PageNumber);
			Assert.Equal(pageSize, result.PageSize);
			Assert.Equal(3, result.TotalItems);
			Assert.Equal(2, result.TotalPages);
			Assert.Empty(result.Items);
		}

		[Fact]
		public async Task GetPagedAsync_PageNumberLessThanOne_ReturnsEmptyItems()
		{
			// Arrange
			int pageNumber = 0;
			int pageSize = 2;

			_repoMock
				.Setup(r => r.Count(It.IsAny<ISpecification>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(3);

			_specFactoryMock
				.Setup(f => f.NoFilterSpecification())
				.Returns(new Mock<ISpecification>().Object);

			// Act
			var result = await _service.GetPagedAsync(pageNumber, pageSize);

			// Assert
			Assert.Equal(pageNumber, result.PageNumber);
			Assert.Equal(pageSize, result.PageSize);
			Assert.Equal(3, result.TotalItems);
			Assert.Equal(2, result.TotalPages);
			Assert.Empty(result.Items);
		}
	}
}
