using congreso.Application.Abstractions.Messaging;
using congreso.Application.UseCase.Users.Comands.LoginRefreshTokenCommand;
using congreso.Application.UseCase.Users.Comands.RevokeRefreshTokenCommand;
using congreso.Application.UseCase.Users.Queries.Login;
using congreso.Application.UseCase.Users.Queries.LoginAdmin;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace congreso.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IDispatcher dispatcher, IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    private readonly IDispatcher _dispatcher = dispatcher;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.Dispatch<LoginQuery, string>(query, cancellationToken);

        if (response.IsSuccess)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false, // True para producción
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path = "/" // <--- Agrega esta línea
            };
            _httpContextAccessor.HttpContext!.Response.Cookies.Append("rt", response.RefreshToken!, cookieOptions);
        }

        response.RefreshToken = "";

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("LoginA")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginAdminQuery query, CancellationToken cancellationToken)
    {
        var response = await _dispatcher.Dispatch<LoginAdminQuery, string>(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("LoginRefreshToken")]
    public async Task<IActionResult> LoginRefreshToken()
    {
        string refreshToken = _httpContextAccessor.HttpContext!.Request.Cookies["rt"]!;

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized("Error en la autenticacion.");
        }

        var command = new LoginRefreshTokenCommand { RefreshToken = refreshToken };

        var response = await _dispatcher.Dispatch<LoginRefreshTokenCommand, string>(command, CancellationToken.None);

        if(response.IsSuccess)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false, // True para producción
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path = "/" // <--- Agrega esta línea
            };
            _httpContextAccessor.HttpContext!.Response.Cookies.Append("rt", response.RefreshToken!, cookieOptions);
        }

        response.RefreshToken = "";

        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("RevokeRefreshToken")]
    public async Task<IActionResult> RevokeRefreshToken()
    {
        var userIdString = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return BadRequest("Error en la autenticacion.");
        }

        if (!int.TryParse(userIdString, out int userId))
        {
            return BadRequest("Error en la autenticacion.");
        }

        var response = await _dispatcher.Dispatch<RevokeRefreshTokenCommand, bool>(new RevokeRefreshTokenCommand() { UserId = userId }, CancellationToken.None);

        _httpContextAccessor.HttpContext.Response.Cookies.Delete("rt");


        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
