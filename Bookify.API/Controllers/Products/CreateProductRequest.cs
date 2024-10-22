namespace Bookify.API.Controllers.Products
{
    public sealed record CreateProductRequest(string Name,
         string Description,
         decimal Price,
         string Merchant,
         byte[]? image);
}