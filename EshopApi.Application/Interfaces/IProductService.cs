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
        /// Retrieves a paginated list of products based on the specified query parameters.
        /// </summary>
        /// <param name="parameters">The query string parameters for pagination and filtering.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a <see cref="PagedList{T}"/> of <see cref="ProductDto"/>.</returns>
        Task<PagedList<ProductDto>> GetPaginatedProductsAsync(QueryStringParameters parameters);
    }
}