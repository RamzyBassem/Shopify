using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Users.Login;
using Shopify.Application.Users.RegisterAdmin;
using Shopify.Application.Users.RegisterUser;

namespace Bookify.API.Controllers.Users
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterRequest request, CancellationToken ct)
        {
            var command = new RegisterUserCommand(request.Email, request.UserName, request.FirstName, request.LastName, request.Password, request.Phone);
            var result = await sender.Send(command, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created(string.Empty, new { result.Value });

        }
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegiserAdmin(RegisterRequest request, CancellationToken ct)
        {
            var command = new RegisterAdminCommand(request.Email, request.UserName, request.FirstName, request.LastName, request.Password, request.Phone);
            var result = await sender.Send(command, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created(string.Empty, new { result.Value });

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
        {
            var command = new LoginUserCommand(request.UserName, request.Password);
            var result = await sender.Send(command, ct);
            return Ok(result.Value);
        }


    }
}
