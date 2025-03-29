using EshopApi.Domain.Entities;

namespace EshopApi.Domain.Interfaces
{
    /// <summary>
    /// Interface for the product repository.
    /// Provides methods to interact with product data.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves a queryable collection of all products.
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/> of <see cref="Product"/> representing all products.</returns>
        IQueryable<Product> GetAllProductsQuery();

        /// <summary>
        /// Retrieves a product by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains the <see cref="Product"/> if found; otherwise, <c>null</c>.</returns>
        Task<Product?> GetProductByIdAsync(Guid id);

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains the updated <see cref="Product"/>.</returns>
        Task<Product> UpdateProductAsync(Product product);
    }
}