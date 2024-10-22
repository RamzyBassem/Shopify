namespace Bookify.API.Controllers.Users
{
    public sealed record LoginRequest(string UserName, string Password);
}
