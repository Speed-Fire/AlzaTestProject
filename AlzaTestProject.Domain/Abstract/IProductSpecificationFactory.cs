using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	/// <summary>
	/// Factory interface for creating <see cref="ISpecification"/> instances
	/// specific to <c>Product</c> entities.
	/// </summary>
	public interface IProductSpecificationFactory
	{
		/// <summary>
		/// Creates a specification to check whether a product with the given name exists.
		/// </summary>
		/// <param name="name">The name of the product.</param>
		/// <returns>An <see cref="ISpecification"/> representing the existence check.</returns>
		ISpecification ExistsByNameSpecification(string name);

		/// <summary>
		/// Creates a specification to retrieve all products in a paginated manner.
		/// </summary>
		/// <param name="page">The page number (starting from 1).</param>
		/// <param name="pageSize">The number of items per page.</param>
		/// <returns>An <see cref="ISpecification"/> representing the paginated query.</returns>
		ISpecification GetAllPagedSpecification(int page, int pageSize);

		/// <summary>
		/// Creates a specification with no filtering criteria, effectively matching all products.
		/// </summary>
		/// <returns>An <see cref="ISpecification"/> that applies no filters.</returns>
		ISpecification NoFilterSpecification();
	}
}
