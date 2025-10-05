using AlzaTestProject.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Specifications
{
	/// <summary>
	/// Represents a specification that can be applied to an <see cref="IQueryable{T}"/> 
	/// of <typeparamref name="TEntity"/> entities for querying purposes.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity to which the specification applies.</typeparam>
	internal interface IOrmSpecification<TEntity> : ISpecification
	{
		/// <summary>
		/// Applies the specification to the given queryable collection of entities.
		/// </summary>
		/// <param name="entities">The <see cref="IQueryable{T}"/> of entities to filter.</param>
		/// <returns>An <see cref="IQueryable{T}"/> with the specification applied.</returns>
		IQueryable<TEntity> Apply(IQueryable<TEntity> entities);
	}
}
