using AlzaTestProject.Domain.Models;
using AlzaTestProject.Services.Dtos;
using OneOf;
using OneOf.Types;

namespace AlzaTestProject.Services.Abstract
{
	public interface IProductService
	{
		Task<OneOf<ProductDto, Error<string>>> CreateAsync(CreateProductDto createProductDto);
		Task<IEnumerable<ProductDto>> GetAllAsync();
		Task<OneOf<ProductDto, NotFound>> GetByIdAsync(int id);
		Task<OneOf<ProductDto, NotFound, Error<string>>> UpdateStockAsync(int id, UpdateStockDto updateStockDto);
	}
}