using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Dtos;
using AlzaTestProject.Services.Misc;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTestProject.Controllers
{
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/Products")]
	public class ProductsControllerV2 : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsControllerV2(IProductService productService)
		{
			_productService = productService;
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
		/// Retrieves a product by its ID.
		/// </summary>
		/// <param name="id">The product ID.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>The requested product.</returns>
		/// <response code="200">The product was found and returned.</response>
		/// <response code="404">No product with the given ID was found.</response>
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
		/// <param name="createProductDto">The product data to create.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>The newly created product.</returns>
		/// <response code="201">The product was successfully created.</response>
		/// <response code="400">The provided data is invalid.</response>
		/// <response code="424">An error occurred while creating the product.</response>
		[HttpPost]
		[ProducesResponseType(typeof(ProductDto), 201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(424)]
		[Produces("application/json")]
		public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto,
			CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
				return ValidationProblem(ModelState);

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

		[HttpPatch("{id}/stock")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(424)]
		[Produces("application/json")]
		public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto stockDto,
			CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
