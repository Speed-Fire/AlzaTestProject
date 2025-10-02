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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
		{
			var products = await _productService.GetAllAsync();
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _productService.GetByIdAsync(id);
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

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
		{
			if (!ModelState.IsValid)
				return ValidationProblem(ModelState);

			var result = await _productService.CreateAsync(createProductDto);
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
		public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto stockDto)
		{
			if (!ModelState.IsValid)
				return ValidationProblem(ModelState);

			var result = await _productService.UpdateStockAsync(id, stockDto);
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
