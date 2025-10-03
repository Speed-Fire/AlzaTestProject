using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Models;
using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AlzaTestProject.Controllers
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		/// <summary>
		/// Retrieves the complete list of products.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token to cancel the request.</param>
		/// <returns>
		/// A collection of <see cref="ProductDto"/> objects.
		/// </returns>
		/// <response code="200">Returns the list of all products.</response>

		[HttpGet]
		[ProducesResponseType(200)]
		[Produces("application/json")]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken)
		{
			var products = await _productService.GetAllAsync(cancellationToken);
			return Ok(products);
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

		/// <summary>
		/// Updates the stock quantity of a product.
		/// </summary>
		/// <param name="id">The ID of the product to update.</param>
		/// <param name="stockDto">The new stock information.</param>
		/// <param name="cancellationToken">Cancellation token to cancel the request.</param>
		/// <returns>
		/// The updated <see cref="ProductDto"/> if the operation was successful.
		/// </returns>
		/// <response code="200">The stock was successfully updated.</response>
		/// <response code="400">The provided stock update data is invalid.</response>
		/// <response code="404">No product with the given ID was found.</response>
		/// <response code="424">An error occurred while updating the stock (e.g., dependency failure).</response>

		[HttpPatch("{id}/stock")]
		[ProducesResponseType(typeof(ProductDto), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(424)]
		[Produces("application/json")]
		public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto stockDto,
			CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
				return ValidationProblem(ModelState);

			var result = await _productService.UpdateStockAsync(id, stockDto, cancellationToken);
			return result.Match<IActionResult>(
				product =>
				{
					return Ok(product);
				},
				notFound =>
				{
					return NotFound();
				},
				error =>
				{
					return UnprocessableEntity(error.Value);
				});
		}
	}
}
