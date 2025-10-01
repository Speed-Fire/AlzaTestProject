using System.ComponentModel.DataAnnotations;

namespace AlzaTestProject.Dtos
{
	public class UpdateStockDto
	{
		[Range(0, int.MaxValue)]
		public int NewStock { get; set; }
	}
}
