using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<PagedList<Order>> GetOrdersAsync(int pageNumber, int pageSize, string? filter, string? sortColumn, string? sortOrder, CancellationToken cancellationToken);
        Task AddAsync(Order order, CancellationToken ct);
        void Delete(Order order);

    }
}
