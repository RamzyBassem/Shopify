using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Orders;
using Shopify.Domain.Products;

namespace Shopify.Infrastructure.Repository
{
    public sealed class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        public async Task AddAsync(Product product, CancellationToken ct)
        {
            await dbContext.Set<Product>().AddAsync(product, ct);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await dbContext.Set<Product>().FirstOrDefaultAsync(product => product.Id == id, ct);
        }

        public async Task<bool> ProductNameExistsAsync(string productName, CancellationToken ct)
        {
            return await dbContext.Set<Product>()
                .AnyAsync(p => p.Name == new Name(productName), ct);
        }
        public void Delete(Product product)
        {
            dbContext.Set<Product>().Remove(product);
        }
        public async Task<PagedList<Product>> GetProductsAsync(int pageNumber, int pageSize, string? filter, string? sortColumn, string? sortOrder, CancellationToken cancellationToken)
        {
            IQueryable<Product> query = dbContext.Set<Product>();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(o => o.Description.Value.Contains(filter) || o.Merchant.Value.Contains(filter) || o.Name.Value.Contains(filter));
            }

            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = sortOrder == "asc"
                    ? query.OrderBy(e => EF.Property<object>(e, sortColumn))
                    : query.OrderByDescending(e => EF.Property<object>(e, sortColumn));
            }

            int totalCount = await query.CountAsync(cancellationToken);

            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(cancellationToken);

            return new PagedList<Product>(items, totalCount, pageNumber, pageSize);
        }
    }
}
