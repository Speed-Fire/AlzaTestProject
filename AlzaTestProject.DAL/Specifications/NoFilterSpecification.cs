using AlzaTestProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Specifications
{
	internal class NoFilterSpecification : IOrmSpecification<ProductEntity>
	{
		public IQueryable<ProductEntity> Apply(IQueryable<ProductEntity> entities)
			=> entities;
	}
}
