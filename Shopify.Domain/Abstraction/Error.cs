namespace Shopify.Domain.Abstraction
{
    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static Error NullValue = new("Error.NullValue", "Null value was provided");
    }
}
