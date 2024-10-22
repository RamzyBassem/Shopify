using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Orders.Shared;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Orders;

namespace Shopify.Application.Orders.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken ct)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, ct);
            if (order is null)
            {
                return Result.Failure<OrderResponse>(OrderErrors.NotFound);
            }
            var orderResponse = MapToOrderResponse(order);
            return Result.Success(orderResponse);
        }

        private OrderResponse MapToOrderResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                TotalCost = order.TotalCost,
                OrderDetailResponse = order.OrderDetails.Select(detail => new OrderDetailDTO
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity
                }).ToList(),
                DeliveryTime = order.DeliveryTime,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate,
                DeliveryAddress = order.DeliveryAddress.Value
            };
        }
    }
}
