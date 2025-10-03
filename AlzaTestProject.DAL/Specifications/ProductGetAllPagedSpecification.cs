using AlzaTestProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Specifications
{
	internal class ProductGetAllPagedSpecification : IOrmSpecification<ProductEntity>
	{
		private readonly int _page;
		private readonly int _pageSize;

		public ProductGetAllPagedSpecification(int page, int pageSize)
		{
			_page = page;
			_pageSize = pageSize;
		}

		public IQueryable<ProductEntity> Apply(IQueryable<ProductEntity> entities)
		{
			return entities
				.Skip((_page - 1) * _pageSize)
				.Take(_pageSize);

		}
	}
}
