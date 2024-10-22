using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Orders.Shared;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Orders;

namespace Shopify.Application.Orders.GetOrders
{
    public sealed class GetOrdersQueryHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrdersQuery, PagedList<OrderResponse>>
    {
        private readonly IOrderRepository orderRepository = orderRepository;

        public async Task<Result<PagedList<OrderResponse>>> Handle(GetOrdersQuery request, CancellationToken ct)
        {
            var orders = await orderRepository.GetOrdersAsync(request.PageNumber, request.PageSize, request.Filter, request.SortColumn, request.SortOrder, ct);

            var orderResponses = orders.Items.Select(order => new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalCost = order.TotalCost,
                DeliveryTime = order.DeliveryTime,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate,
                DeliveryAddress = order.DeliveryAddress.Value
            }).ToList();
            var pagedOrderResponse = new PagedList<OrderResponse>(orderResponses, orders.Page, orders.PageSize, orders.TotalCount);

            return Result.Success(pagedOrderResponse);
        }
    }
}
