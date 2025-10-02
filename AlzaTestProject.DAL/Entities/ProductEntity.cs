using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Entities
{
	internal class ProductEntity : BaseEntity
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string ImageUrl { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int Stock { get; set; }
	}
}
