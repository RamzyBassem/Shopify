using Shopify.Domain.Users;

namespace Shopify.Domain.Abstraction
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
