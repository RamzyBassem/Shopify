using Shopify.Domain.Users;

namespace Shopify.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
