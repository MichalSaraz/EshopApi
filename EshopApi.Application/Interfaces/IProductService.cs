using EshopApi.Domain.Entities;
using EshopApi.Shared.Dtos;
using EshopApi.Shared.Models;

namespace EshopApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> UpdateProductDescriptionAsync(Guid id, string description);
        Task<PagedList<ProductDto>> GetPaginatedProductsAsync(QueryStringParameters parameters);
    }
}