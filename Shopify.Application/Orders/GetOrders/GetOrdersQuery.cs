using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Orders.Shared;
using Shopify.Domain.Abstraction;

namespace Shopify.Application.Orders.GetOrders
{
    public sealed record GetOrdersQuery(int PageNumber, int PageSize, string? Filter, string? SortColumn, string? SortOrder) : IQuery<PagedList<OrderResponse>>;

}
