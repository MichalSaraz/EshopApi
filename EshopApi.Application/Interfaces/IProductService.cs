using EshopApi.Domain.Entities;
using EshopApi.Shared.Dtos;
using EshopApi.Shared.Models;

namespace EshopApi.Application.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Updates the description of a product identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <param name="description">The new description for the product.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains the <see cref="ProductDto"/> with the updated description.</returns>
        Task<ProductDto> UpdateProductDescriptionAsync(Guid id, string description);

        /// <summary>
        /// Retrieves a paginated list of products based on the specified query string parameters.
        /// </summary>
        /// <param name="parameters">The query string parameters used for pagination and filtering.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a tuple where:
        /// - The first item is a list of <see cref="ProductDto"/> representing the paginated products.
        /// - The second item is an integer representing the total number of products available.
        /// </returns>
        Task<(List<ProductDto>, int)> GetPaginatedProductsAsync(QueryStringParameters parameters);
    }
}