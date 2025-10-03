using AlzaTestProject.Domain.Models;
using AlzaTestProject.Services.Dtos;
using AlzaTestProject.Services.Requests;

namespace AlzaTestProject.Services.Extensions
{
	public static class ModelMappingExtensions
	{
		public static ProductDto MapToDto(this Product product)
		{
			return new()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description ?? string.Empty,
				Price = product.Price,
				ImageUrl = product.ImageUrl.AbsoluteUri,
				Stock = product.Stock,
			};
		}

		public static UpdateStockRequest MapToRequest(this UpdateStockDto dto, int id)
		{
			return new(id, dto.NewStock);
		}
	}
}
