using Asp.Versioning;
using Bookify.API.Controllers.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Orders.CreateOrder;
using Shopify.Application.Orders.DeleteOrder;
using Shopify.Application.Orders.GetOrderById;
using Shopify.Application.Orders.UpdateOrder;

namespace Bookify.API.Controllers.Orders
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid id, CancellationToken ct)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await sender.Send(query, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request, CancellationToken ct)
        {
            var command = new CreateOrderCommand(request.OrderDetails, request.UserId, request.DeliveryTime, request.DeliveryAddress);
            var result = await sender.Send(command, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created(string.Empty, result.Value);

        }
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderRequest request, CancellationToken ct)
        {
            if (id != request.OrderId)
            {
                return BadRequest("Product ID in the URL does not match the ID in the request body.");
            }

            var command = new UpdateOrderCommand(request.OrderId, request.UserId, request.OrderDetails, request.DeliveryTime, request.DeliveryAddress);
            var result = await sender.Send(command, ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken ct)
        {
            var command = new DeleteOrderCommand(id);
            var result = await sender.Send(command, ct);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }
    }
}
