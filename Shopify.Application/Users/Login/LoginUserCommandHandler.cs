using Shopify.Application.Abstractions.Authentication;
using Shopify.Application.Abstractions.Messaging;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Users;

namespace Shopify.Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken ct)
    {

        var user = await userRepository.GetByUserNameAsync(command.UserName, ct);
        if (user is null)
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash.Value);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.InvalidCredentials);
        }

        string token = tokenProvider.Create(user);

        return token;
    }
}
