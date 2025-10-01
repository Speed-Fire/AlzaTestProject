using System.ComponentModel.DataAnnotations;

namespace AlzaTestProject.Services.Dtos
{
	public class UpdateStockDto
	{
		[Range(0, int.MaxValue)]
		public int NewStock { get; set; }
	}
}
