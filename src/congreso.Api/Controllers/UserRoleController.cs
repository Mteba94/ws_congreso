using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Dtos.Roles;
using congreso.Application.Dtos.UserRoles;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetById;
using congreso.Application.UseCase.UserRoles.Commands.Create;
using congreso.Application.UseCase.UserRoles.Commands.Delete;
using congreso.Application.UseCase.UserRoles.Commands.Update;
using congreso.Application.UseCase.UserRoles.Queries.GetAll;
using congreso.Application.UseCase.UserRoles.Queries.GetById;
using congreso.Application.UseCase.UserRoles.Queries.GetByUserId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> UserRoleList([FromQuery] GetAllUserRoleQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllUserRoleQuery, IEnumerable<UserRoleResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{userRoleId:int}")]
        public async Task<IActionResult> UserRoleById(int userRoleId)
        {
            var response = await _dispatcher.Dispatch<GetUserRoleByIdQuery, UserRoleByIdResponseDto>
                (new GetUserRoleByIdQuery() { UserRoleId = userRoleId }, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Role/{userId:int}")]
        public async Task<IActionResult> UserRoleByUserId(int userId)
        {
            var response = await _dispatcher
                .Dispatch<GetUserRoleByUserIdQuery, RoleByIdResponseDto>(new GetUserRoleByUserIdQuery { UserId = userId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> UserRoleCreate([FromBody] CreateUserRoleCommand command)
        {
            var response = await _dispatcher.Dispatch<CreateUserRoleCommand, bool>
              (command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UserRoleUpdate([FromBody] UpdateUserRoleCommand command)
        {
            var response = await _dispatcher.Dispatch<UpdateUserRoleCommand, bool>
              (command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Delete/{userRoleId:int}")]
        public async Task<IActionResult> UserRoleDelete(int userRoleId)
        {
            var response = await _dispatcher.Dispatch<DeleteUserRoleCommand, bool>
              (new DeleteUserRoleCommand { UserRoleId = userRoleId}, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
