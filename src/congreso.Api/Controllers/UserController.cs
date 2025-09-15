using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Application.UseCase.Users.Comands.DeleteUser;
using congreso.Application.UseCase.Users.Comands.UpdateUser;
using congreso.Application.UseCase.Users.Queries.GetAllUser;
using congreso.Application.UseCase.Users.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Authorize]
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

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> UserById(int userId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdUserQuery, UserByIdResponseDto>(new GetByIdUserQuery { UserId = userId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateUserCommand, bool>(command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateUserCommand, bool>(command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var response = await _dispatcher
                .Dispatch<DeleteUserCommand, bool>(new DeleteUserCommand { UserId = userId }, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
