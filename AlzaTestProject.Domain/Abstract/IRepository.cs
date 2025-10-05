using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	/// <summary>
	/// Represents a generic repository for managing entities of type <typeparamref name="T"/>.
	/// Provides basic CRUD operations and querying by specifications.
	/// </summary>
	/// <typeparam name="T">The type of entity managed by the repository.</typeparam>
	public interface IRepository<T>
	{
		/// <summary>
		/// Counts the number of entities that match the given specification.
		/// </summary>
		/// <param name="specification">The specification used to filter entities.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>The number of entities that match the specification.</returns>
		Task<int> Count(ISpecification specification, CancellationToken cancellationToken);

		/// <summary>
		/// Retrieves all entities from the repository.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>A collection of all entities.</returns>
		Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves all entities that match the given specification.
		/// </summary>
		/// <param name="specification">The specification used to filter entities.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>A collection of entities that match the specification.</returns>
		Task<IEnumerable<T>> GetAll(ISpecification specification, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves an entity by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the entity.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>The entity with the specified ID, or <c>null</c> if not found.</returns>
		Task<T?> GetById(int id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Checks whether any entities match the given specification.
		/// </summary>
		/// <param name="specification">The specification used to filter entities.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns><c>true</c> if any entities match the specification; otherwise, <c>false</c>.</returns>
		Task<bool> Exists(ISpecification specification, CancellationToken cancellationToken = default);

		/// <summary>
		/// Adds a new entity to the repository.
		/// </summary>
		/// <param name="item">The entity to add.</param>
		/// <returns>The added entity.</returns>
		T Add(T item);

		/// <summary>
		/// Updates an existing entity in the repository.
		/// </summary>
		/// <param name="item">The entity to update.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>The updated entity.</returns>
		Task<T> Update(T item, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes an entity from the repository.
		/// </summary>
		/// <param name="item">The entity to delete.</param>
		void Delete(T item);
	}
}
