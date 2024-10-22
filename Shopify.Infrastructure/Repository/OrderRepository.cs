using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Orders;
using Shopify.Infrastructure;

internal class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
{
    private readonly ApplicationDbContext dbContext = dbContext;

    public async Task AddAsync(Order order, CancellationToken ct)
    {
        await dbContext.Set<Order>().AddAsync(order, ct);
    }

    public void Delete(Order order)
    {
        dbContext.Set<Order>().Remove(order);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.Set<Order>().Include(o => o.OrderDetails).FirstOrDefaultAsync(order => order.Id == id, ct);
    }

    public async Task<PagedList<Order>> GetOrdersAsync(int pageNumber, int pageSize, string? filter, string? sortColumn, string? sortOrder, CancellationToken cancellationToken)
    {
        IQueryable<Order> query = dbContext.Set<Order>().Include(o => o.OrderDetails);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(o => o.DeliveryAddress == new DeliveryAddress(filter));
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

        return new PagedList<Order>(items, totalCount, pageNumber, pageSize);
    }
}
