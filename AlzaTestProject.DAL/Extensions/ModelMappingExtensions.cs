using AlzaTestProject.DAL.Entities;
using AlzaTestProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Extensions
{
	internal static class ModelMappingExtensions
	{
		public static Product MapToDom(this ProductEntity entity)
		{
			return new(
				entity.Id,
				entity.Name,
				entity.Description, 
				entity.ImageUrl, 
				entity.Price,
				entity.Stock);
		}

		public static ProductEntity MapToDal(this Product product)
		{
			var entity = new ProductEntity();
			return product.MapToDal(entity);
		}

		public static ProductEntity MapToDal(this Product product, ProductEntity entity)
		{
			entity.Id = product.Id;
			entity.Name = product.Name;
			entity.Description = product.Description;
			entity.ImageUrl = product.ImageUrl.AbsoluteUri;
			entity.Price = product.Price;
			entity.Stock = product.Stock;

			return entity;
		}
	}
}
