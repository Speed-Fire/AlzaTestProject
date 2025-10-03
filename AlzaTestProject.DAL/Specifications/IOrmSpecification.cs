using AlzaTestProject.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Specifications
{
	internal interface IOrmSpecification<TEntity> : ISpecification
	{
		IQueryable<TEntity> Apply(IQueryable<TEntity> entities);
	}
}
