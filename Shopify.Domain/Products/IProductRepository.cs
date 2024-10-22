using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Products
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, CancellationToken ct);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<PagedList<Product>> GetProductsAsync(int pageNumber, int pageSize, string? filter, string? sortColumn, string? sortOrder, CancellationToken cancellationToken);
        Task<bool> ProductNameExistsAsync(string productName, CancellationToken ct);
        void Delete(Product product);
    }
}
