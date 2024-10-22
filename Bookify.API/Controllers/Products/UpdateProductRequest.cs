namespace Bookify.API.Controllers.Products
{
    public sealed record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price, string Merchant);

}
