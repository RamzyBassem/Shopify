using Shopify.Application.Abstractions.Messaging;

namespace Shopify.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Email, string UserName, string FirstName, string LastName, string Password, string Phone)
    : ICommand<Guid>;
