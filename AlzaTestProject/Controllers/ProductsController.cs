using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Models;
using AlzaTestProject.Services.Abstract;
using AlzaTestProject.Services.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AlzaTestProject.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		/// <summary>
		/// Retrieves all products.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>A collection of products.</returns>
		/// <response code="200">Returns the list of products.</response>
		[HttpGet]
		[ProducesResponseType(200)]
		[Produces("application/json")]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken)
		{
			var products = await _productService.GetAllAsync(cancellationToken);
			return Ok(products);
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

		/// <summary>
		/// Updates the stock quantity of a product.
		/// </summary>
		/// <param name="id">The product ID.</param>
		/// <param name="stockDto">The updated stock data.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>The updated product.</returns>
		/// <response code="200">The stock quantity was successfully updated.</response>
		/// <response code="400">The provided data is invalid.</response>
		/// <response code="404">No product with the given ID was found.</response>
		/// <response code="424">An error occurred while updating the stock quantity.</response>
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
