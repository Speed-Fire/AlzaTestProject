using AlzaTestProject.Controllers;
using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Tests
{
	public class ProductsControllerTests
	{
		private readonly Mock<IProductService> _productServiceMock;
		private readonly ProductsController _controller;

		public ProductsControllerTests()
		{
			_productServiceMock = new Mock<IProductService>();
			_controller = new ProductsController(_productServiceMock.Object);
		}

		[Fact]
		public async Task GetAll_ReturnsOk_WithListOfProducts()
		{
			// Arrange
			var products = new List<ProductDto>
			{
				new ProductDto { Id = 1, Name = "Test1" },
				new ProductDto { Id = 2, Name = "Test2" }
			};

			_productServiceMock
				.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(products);

			// Act
			var result = await _controller.GetAll(CancellationToken.None);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnProducts = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
			Assert.Equal(2, ((List<ProductDto>)returnProducts).Count);
		}

		[Fact]
		public async Task GetById_ProductExists_ReturnsOk()
		{
			// Arrange
			var product = new ProductDto { Id = 1, Name = "Test" };

			_productServiceMock
				.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>()))
				.ReturnsAsync(product);

			// Act
			var result = await _controller.GetById(1, CancellationToken.None);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnProduct = Assert.IsType<ProductDto>(okResult.Value);
			Assert.Equal(1, returnProduct.Id);
		}

		[Fact]
		public async Task GetById_ProductNotFound_ReturnsNotFound()
		{
			// Arrange
			_productServiceMock
				.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new NotFound());

			// Act
			var result = await _controller.GetById(1, CancellationToken.None);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task Create_ValidProduct_ReturnsCreatedAtAction()
		{
			// Arrange
			var createDto = new CreateProductDto { Name = "NewProduct", ImageUrl = "http://example.com/image.png" };
			var product = new ProductDto { Id = 1, Name = createDto.Name, ImageUrl = createDto.ImageUrl };

			_productServiceMock
				.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>()))
				.ReturnsAsync(product);

			// Act
			var result = await _controller.Create(createDto, CancellationToken.None);

			// Assert
			var createdResult = Assert.IsType<CreatedAtActionResult>(result);
			var returnProduct = Assert.IsType<ProductDto>(createdResult.Value);
			Assert.Equal(1, returnProduct.Id);
		}

		[Fact]
		public async Task Create_InvalidModel_ReturnsValidationProblem()
		{
			// Arrange
			_controller.ModelState.AddModelError("Name", "Required");
			var createDto = new CreateProductDto();

			// Act
			var result = await _controller.Create(createDto, CancellationToken.None);

			// Assert
			Assert.IsType<ValidationProblemDetails>(Assert.IsType<ObjectResult>(result).Value);
		}

		[Fact]
		public async Task UpdateStock_ProductExists_ReturnsOk()
		{
			// Arrange
			var updateDto = new UpdateStockDto { NewStock = 10 };
			var product = new ProductDto { Id = 1, Stock = 10 };

			_productServiceMock
				.Setup(s => s.UpdateStockAsync(1, updateDto, It.IsAny<CancellationToken>()))
				.ReturnsAsync(product);

			// Act
			var result = await _controller.UpdateStock(1, updateDto, CancellationToken.None);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnProduct = Assert.IsType<ProductDto>(okResult.Value);
			Assert.Equal(10, returnProduct.Stock);
		}

		[Fact]
		public async Task UpdateStock_ProductNotFound_ReturnsNotFound()
		{
			// Arrange
			var updateDto = new UpdateStockDto { NewStock = 10 };
			_productServiceMock
				.Setup(s => s.UpdateStockAsync(1, updateDto, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new NotFound());

			// Act
			var result = await _controller.UpdateStock(1, updateDto, CancellationToken.None);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task UpdateStock_InvalidModel_ReturnsValidationProblem()
		{
			// Arrange
			_controller.ModelState.AddModelError("NewStock", "Invalid");
			var updateDto = new UpdateStockDto();

			// Act
			var result = await _controller.UpdateStock(1, updateDto, CancellationToken.None);

			// Assert
			Assert.IsType<ValidationProblemDetails>(Assert.IsType<ObjectResult>(result).Value);
		}
	}
}
