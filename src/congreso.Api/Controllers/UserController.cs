using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Users.Queries.GetAllUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> UserList([FromQuery] GetAllUserQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllUserQuery, IEnumerable<UserResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
