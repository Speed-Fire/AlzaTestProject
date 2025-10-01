using System.ComponentModel.DataAnnotations;

namespace AlzaTestProject.Dtos
{
	public class CreateProductDto
	{
		[Required]
		public string Name { get; set; } = string.Empty;

		[Required]
		[Url]
		public string ImageUrl { get; set; } = string.Empty;
	}
}
