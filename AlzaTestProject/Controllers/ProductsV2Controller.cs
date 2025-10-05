using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Requests;
using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Dtos;
using AlzaTestProject.Services.Extensions;
using AlzaTestProject.Services.Misc;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTestProject.Controllers
{
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/Products")]
	public class ProductsV2Controller : ControllerBase
	{
		private readonly IAsyncQueue<UpdateStockRequest> _queue;
		private readonly IProductService _productService;
		private readonly ILogger _logger;

		public ProductsV2Controller(
			IAsyncQueue<UpdateStockRequest> queue, 
			IProductService productService,
			ILogger<ProductsV2Controller> logger)
		{
			_queue = queue;
			_productService = productService;
			_logger = logger;
		}

		/// <summary>
		/// Retrieves a paginated list of products.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token to cancel the request.</param>
		/// <param name="pageNum">
		/// The page number to retrieve. Defaults to 1 if not specified.
		/// Must be a positive integer.
		/// </param>
		/// <param name="pageSize">
		/// The number of products per page. Defaults to 10 if not specified.
		/// Must be a positive integer.
		/// </param>
		/// <returns>
		/// A <see cref="PagedResult{ProductDto}"/> containing the products for the requested page.
		/// </returns>
		/// <response code="200">Returns the paginated list of products in JSON format.</response>
		[HttpGet]
		[ProducesResponseType(200)]
		[Produces("application/json")]
		public async Task<ActionResult<PagedResult<ProductDto>>> GetAll(
			CancellationToken cancellationToken,
			[FromQuery] int pageNum = 1,
			[FromQuery] int pageSize = 10)
		{
			var result = await _productService.GetPagedAsync(pageNum, pageSize);
			return Ok(result);
		}

		/// <summary>
		/// Retrieves a single product by its unique identifier.
		/// </summary>
		/// <param name="id">The ID of the product to retrieve.</param>
		/// <param name="cancellationToken">Cancellation token to cancel the request.</param>
		/// <returns>
		/// A <see cref="ProductDto"/> if the product is found; otherwise, a <c>404 Not Found</c>.
		/// </returns>
		/// <response code="200">The product was found and returned.</response>
		/// <response code="404">No product with the given ID exists.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(404)]
		[Produces("application/json")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			var result = await _productService.GetByIdAsync(id, cancellationToken);
			return result.Match<IActionResult>(
				product =>
				{
					return Ok(product);
				},
				notFound =>
				{
					return NotFound();
				});
		}

		/// <summary>
		/// Creates a new product.
		/// </summary>
		/// <param name="createProductDto">The data used to create the product.</param>
		/// <param name="cancellationToken">Cancellation token to cancel the request.</param>
		/// <returns>
		/// The newly created <see cref="ProductDto"/> with its assigned ID.
		/// </returns>
		/// <response code="201">The product was successfully created.</response>
		/// <response code="400">The provided product data is invalid.</response>
		/// <response code="424">
		/// A failure occurred while creating the product (e.g., external dependency error).
		/// </response>
		[HttpPost]
		[ProducesResponseType(typeof(ProductDto), 201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(424)]
		[Produces("application/json")]
		public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto,
			CancellationToken cancellationToken)
		{
			var result = await _productService.CreateAsync(createProductDto, cancellationToken);
			return result.Match<IActionResult>(
				product =>
				{
					return CreatedAtAction(
						nameof(GetById),
						new { id = product.Id },
						product);
				},
				error =>
				{
					return UnprocessableEntity(error.Value);
				});
		}

		/// <summary>
		/// Enqueues a request to update the stock quantity of a product.
		/// </summary>
		/// <param name="id">The ID of the product to update.</param>
		/// <param name="stockDto">The stock update data (new stock quantity).</param>
		/// <param name="cancellationToken">Cancellation token to cancel the request.</param>
		/// <returns>
		/// Returns <c>202 Accepted</c> when the request has been successfully enqueued for processing.
		/// </returns>
		/// <response code="202">
		/// The stock update request was accepted and queued for asynchronous processing.
		/// </response>
		/// <response code="400">
		/// The provided data is invalid (validation errors).
		/// </response>
		[HttpPatch("{id}/stock")]
		[ProducesResponseType(202)]
		[Produces("application/json")]
		public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto stockDto,
			CancellationToken cancellationToken)
		{
			var updateRequest = stockDto.MapToRequest(id);
			await _queue.EnqueueAsync(updateRequest, cancellationToken);

			_logger.LogInformation("Enqueued stock update for ProductId={ProductId}, NewStock={NewStock}",
				id, stockDto.NewStock);

			return Accepted();
		}
	}
}
