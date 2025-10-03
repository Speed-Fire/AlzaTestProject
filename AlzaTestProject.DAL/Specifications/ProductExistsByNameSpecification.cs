using AlzaTestProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Specifications
{
	internal class ProductExistsByNameSpecification : IOrmSpecification<ProductEntity>
	{
		private readonly string _name;

		public ProductExistsByNameSpecification(string name)
		{
			_name = name;
		}

		public IQueryable<ProductEntity> Apply(IQueryable<ProductEntity> entities)
		{
			return entities
				.Where(e => e.Name == _name);
		}
	}
}
