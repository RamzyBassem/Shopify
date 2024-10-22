using Shopify.Application.Abstractions.Messaging;

namespace Shopify.Application.Users.Login;

public sealed record LoginUserCommand(string UserName, string Password) : ICommand<string>;
