using AlzaTestProject.Domain.Models;
using AlzaTestProject.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlzaTestProject.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		[HttpGet]
		public ActionResult<IEnumerable<ProductDto>> GetAll()
		{
			throw new NotImplementedException();
		}

		[HttpGet("{id}")]
		public ActionResult<ProductDto> GetById(int id)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateProductDto createProductDto)
		{
			throw new NotImplementedException();
		}

		[HttpPatch("{id}")]
		public IActionResult UpdateStock(int id, [FromBody] UpdateStockDto stockDto)
		{
			throw new NotImplementedException();
		}
	}
}
