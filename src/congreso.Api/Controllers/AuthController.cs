using congreso.Application.Abstractions.Messaging;
using congreso.Application.UseCase.Users.Queries.Login;
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
}
