using AlzaTestProject.Domain.Models;
using AlzaTestProject.Services.Dtos;
using AlzaTestProject.Services.Misc;
using OneOf;
using OneOf.Types;

namespace AlzaTestProject.Services.Abstract
{
	/// <summary>
	/// Provides operations for managing <c>Product</c> entities,
	/// including creation, retrieval, pagination, and stock updates.
	/// </summary>
	public interface IProductService
	{
		/// <summary>
		/// Creates a new product asynchronously.
		/// </summary>
		/// <param name="createProductDto">The data transfer object containing product creation details.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>
		/// A <see cref="OneOf{T0,T1}"/> containing the created <see cref="ProductDto"/> if successful,
		/// or an <see cref="Error{String}"/> describing the failure.
		/// </returns>
		Task<OneOf<ProductDto, Error<string>>> CreateAsync(CreateProductDto createProductDto, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves all products asynchronously.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>A collection of <see cref="ProductDto"/> representing all products.</returns>
		Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves products in a paginated manner asynchronously.
		/// </summary>
		/// <param name="pageNumber">The page number to retrieve (starting from 1).</param>
		/// <param name="pageSize">The number of items per page.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>A <see cref="PagedResult{T}"/> containing the products for the specified page.</returns>
		Task<PagedResult<ProductDto>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves a product by its unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the product.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>
		/// A <see cref="OneOf{T0,T1}"/> containing the <see cref="ProductDto"/> if found,
		/// or <see cref="NotFound"/> if the product does not exist.
		/// </returns>
		Task<OneOf<ProductDto, NotFound>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Updates the stock quantity of a product asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the product.</param>
		/// <param name="updateStockDto">The data transfer object containing stock update details.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
		/// <returns>
		/// A <see cref="OneOf{T0,T1,T2}"/> containing the updated <see cref="ProductDto"/> if successful,
		/// <see cref="NotFound"/> if the product does not exist, or
		/// <see cref="Error{String}"/> if the operation fails.
		/// </returns>
		Task<OneOf<ProductDto, NotFound, Error<string>>> UpdateStockAsync(int id, UpdateStockDto updateStockDto, CancellationToken cancellationToken = default);
	}
}