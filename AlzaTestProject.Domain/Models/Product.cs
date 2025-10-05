using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public Uri ImageUrl { get; set; }
		public decimal Price { get; private set; }
		public int Stock { get; private set; }

		public Product(string name, string imageUrl)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));
			if (!Uri.TryCreate(imageUrl, UriKind.Absolute, out var url))
				throw new ArgumentException("Given image url is not a vaild url.", nameof(imageUrl));

			Name = name;
			ImageUrl = url;
		}

		public Product(int id, string name, string? description, string imageUrl, decimal price, int stock)
			:
			this(name, imageUrl)
		{
			Id = id;
			Description = description;

			UpdatePrice(price);
			UpdateStock(stock);
		}

		public void UpdateStock(int value)
		{
			if (value < 0)
				throw new ArgumentException("Stock cannot be negative.", nameof(value));

			Stock = value;
		}

		public void UpdatePrice(decimal value)
		{
			if(value < 0 ) 
				throw new ArgumentException("Price cannot be negative.", nameof(value));

			Price = value;
		}
	}
}
