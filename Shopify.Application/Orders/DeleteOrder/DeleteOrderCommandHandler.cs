using Shopify.Application.Abstractions.Messaging;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Orders;

namespace Shopify.Application.Orders.DeleteOrder
{
    public class DeleteOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository orderRepository = orderRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken ct)
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId, ct);
            if (order is null)
                return Result.Failure(OrderErrors.NotFound);
            orderRepository.Delete(order);
            await unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }
    }
}
