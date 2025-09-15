using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Roles;
using congreso.Application.UseCase.Roles.Commands.Create;
using congreso.Application.UseCase.Roles.Commands.Delete;
using congreso.Application.UseCase.Roles.Commands.Update;
using congreso.Application.UseCase.Roles.Queries.GetAll;
using congreso.Application.UseCase.Roles.Queries.GetById;
using congreso.Application.UseCase.Roles.Queries.GetSelect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> RoleList([FromQuery] GetAllRoleQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllRoleQuery, IEnumerable<RoleResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> RoleSelect()
        {
            var response = await _dispatcher
                .Dispatch<GetSelectRoleQuery, IEnumerable<SelectResponseDto>>(new GetSelectRoleQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{roleId:int}")]
        public async Task<IActionResult> RoleById(int roleId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdRoleQuery, RoleByIdResponseDto>(new GetByIdRoleQuery { RoleId = roleId}, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> RolCreate([FromBody] CreateRoleCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateRoleCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> RoleUpdate([FromBody] UpdateRoleCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateRoleCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Delete/{roleId:int}")]
        public async Task<IActionResult> RoleDelete(int roleId)
        {
            var response = await _dispatcher
                .Dispatch<DeleteRoleCommand, bool>(new DeleteRoleCommand { RoleId = roleId }, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
