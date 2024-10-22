using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Products.CreateProduct;
using Shopify.Application.Products.DeleteProduct;
using Shopify.Application.Products.GetProductById;
using Shopify.Application.Products.UpdateProduct;

namespace Bookify.API.Controllers.Products
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id, CancellationToken ct)
        {
            var query = new GetProductByIdQuery(id);
            var result = await sender.Send(query, ct);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request, CancellationToken ct)
        {
            var command = new CreateProductCommand(request.Name, request.Description, request.Price, request.Merchant,request.image);
            var result = await sender.Send(command, ct);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetProduct), new { id = result.Value }, result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest request, CancellationToken ct)
        {
            if (id != request.Id)
            {
                return BadRequest("Product ID in the URL does not match the ID in the request body.");
            }

            var command = new UpdateProductCommand(request.Id, request.Name, request.Description, request.Price, request.Merchant);
            var result = await sender.Send(command, ct);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken ct)
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command, ct);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return NoContent();
        }
    }
}
