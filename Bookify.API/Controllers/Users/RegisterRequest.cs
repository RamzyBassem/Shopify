namespace Bookify.API.Controllers.Users
{
    public sealed record RegisterRequest(string FirstName,
         string LastName,
         string Email,
         string Password,
         string Phone,
         string UserName
        );

}