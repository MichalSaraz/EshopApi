using EshopApi.Domain.Entities;

namespace EshopApi.Domain.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAllProductsQuery();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<Product> UpdateProductAsync(Product product);
    }
}