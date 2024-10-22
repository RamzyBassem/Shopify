using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Products
{
    public static class ProductErrors
    {
        public static Error NotFound = new(
            "Product.NotFound",
            "The product with the specified identifier was not found");
        public static Error NameAlreadyExists = new(
            "Product.NameExists",
            "This product name already exists in the db");
    }
}
