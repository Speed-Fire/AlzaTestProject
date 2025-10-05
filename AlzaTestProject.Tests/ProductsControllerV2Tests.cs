using AlzaTestProject.Controllers;
using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Requests;
using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Extensions;
using AlzaTestProject.Services.Dtos;
using AlzaTestProject.Services.Misc;
using Microsoft.AspNetCore.Mvc;
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
	public class ProductsControllerV2Tests
	{
		private readonly Mock<IProductService> _productServiceMock;
		private readonly Mock<IAsyncQueue<UpdateStockRequest>> _queueMock;
		private readonly Mock<ILogger<ProductsControllerV2>> _loggerMock;
		private readonly ProductsControllerV2 _controller;

		public ProductsControllerV2Tests()
		{
			_productServiceMock = new Mock<IProductService>();
			_queueMock = new Mock<IAsyncQueue<UpdateStockRequest>>();
			_loggerMock = new Mock<ILogger<ProductsControllerV2>>();

			_controller = new ProductsControllerV2(
				_queueMock.Object,
				_productServiceMock.Object,
				_loggerMock.Object);
		}

		[Fact]
		public async Task GetAll_ReturnsOkResult_WithPagedProducts()
		{
			// Arrange
			var pagedResult = new PagedResult<ProductDto>
			{
				Items = [new ProductDto { Id = 1, Name = "TestProduct" }],
				TotalItems = 1,
			};

			_productServiceMock
				.Setup(s => s.GetPagedAsync(1, 10, CancellationToken.None))
				.ReturnsAsync(pagedResult);

			// Act
			var result = await _controller.GetAll(CancellationToken.None);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var returnedValue = Assert.IsType<PagedResult<ProductDto>>(okResult.Value);
			Assert.Single(returnedValue.Items);
			Assert.Equal(1, returnedValue.TotalItems);
		}

		[Fact]
		public async Task GetById_ProductExists_ReturnsOkResult()
		{
			// Arrange
			var product = new ProductDto { Id = 1, Name = "TestProduct" };
			_productServiceMock
				.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>()))
				.ReturnsAsync(product);

			// Act
			var result = await _controller.GetById(1, CancellationToken.None);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedProduct = Assert.IsType<ProductDto>(okResult.Value);
			Assert.Equal(product.Id, returnedProduct.Id);
			Assert.Equal(product.Name, returnedProduct.Name);
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
		public async Task Create_ProductCreated_ReturnsCreatedAtAction()
		{
			// Arrange
			var createDto = new CreateProductDto { Name = "NewProduct" };
			var createdProduct = new ProductDto { Id = 1, Name = "NewProduct" };

			_productServiceMock
				.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>()))
				.ReturnsAsync(createdProduct);

			// Act
			var result = await _controller.Create(createDto, CancellationToken.None);

			// Assert
			var createdResult = Assert.IsType<CreatedAtActionResult>(result);
			var returnedProduct = Assert.IsType<ProductDto>(createdResult.Value);
			Assert.Equal(createdProduct.Id, returnedProduct.Id);
			Assert.Equal(createdProduct.Name, returnedProduct.Name);
		}

		[Fact]
		public async Task Create_ProductCreationFailed_ReturnsUnprocessableEntity()
		{
			// Arrange
			var createDto = new CreateProductDto { Name = "InvalidProduct" };
			var error = new Error<string>("Creation failed");

			_productServiceMock
				.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>()))
				.ReturnsAsync(error);

			// Act
			var result = await _controller.Create(createDto, CancellationToken.None);

			// Assert
			var unprocessableResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
			Assert.Equal(error.Value, unprocessableResult.Value);
		}

		[Fact]
		public async Task UpdateStock_EnqueuesRequest_ReturnsAccepted()
		{
			// Arrange
			var stockDto = new UpdateStockDto { NewStock = 42 };
			var productId = 1;
			var updateRequest = stockDto.MapToRequest(productId);

			_queueMock
				.Setup(q => q.EnqueueAsync(It.IsAny<UpdateStockRequest>(), It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _controller.UpdateStock(productId, stockDto, CancellationToken.None);

			// Assert
			_queueMock.Verify(q => q.EnqueueAsync(It.Is<UpdateStockRequest>(
				r => r.ProductId == productId && r.NewStock == stockDto.NewStock),
				It.IsAny<CancellationToken>()), Times.Once);

			var acceptedResult = Assert.IsType<AcceptedResult>(result);
		}
	}
}
