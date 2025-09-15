using congreso.Application.Abstractions.Messaging;
using congreso.Application.UseCase.Users.Comands.LoginRefreshTokenCommand;
using congreso.Application.UseCase.Users.Comands.RevokeRefreshTokenCommand;
using congreso.Application.UseCase.Users.Queries.Login;
using congreso.Application.UseCase.Users.Queries.LoginAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IDispatcher dispatcher) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.Dispatch<LoginQuery, string>(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("LoginA")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminQuery query, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.Dispatch<LoginAdminQuery, string>(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("LoginRefreshToken")]
    public async Task<IActionResult> LoginRefreshToken([FromBody] LoginRefreshTokenCommand request)
    {
        var response = await _dispatcher.Dispatch<LoginRefreshTokenCommand, string>(request, CancellationToken.None);
        return Ok(response);
    }

    [HttpDelete("RevokeRefreshToken/{userId:int}")]
    public async Task<IActionResult> RevokeRefreshToken(int userId)
    {
        var response = await _dispatcher.Dispatch<RevokeRefreshTokenCommand, bool>
            (new RevokeRefreshTokenCommand() { UserId = userId }, CancellationToken.None);
        return Ok(response);
    }
}
