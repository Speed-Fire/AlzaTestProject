using AlzaTestProject.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Specifications.Factories
{
	internal class ProductSpecificationFactory : IProductSpecificationFactory
	{
		public ISpecification ExistsByNameSpecification(string name)
		{
			return new ProductExistsByNameSpecification(name);
		}

		public ISpecification GetAllPagedSpecificatyion(int page, int pageSize)
		{
			return new ProductGetAllPagedSpecification(page, pageSize);
		}

		public ISpecification NoFilterSpecification()
		{
			return new NoFilterSpecification();
		}
	}
}
