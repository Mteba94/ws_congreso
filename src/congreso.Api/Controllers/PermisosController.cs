using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Permisos;
using congreso.Application.UseCase.Permisos.Queries.GetPermissionsByRoleId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("PermissionByRoleId/{roleId:int}")]
        public async Task<IActionResult> GetPermissionsByRoleId(int roleId)
        {
            var response = await _dispatcher.Dispatch<GetPermissionsByRoleIdQuery, IEnumerable<PermissionsByRoleResponseDto>>
                (new GetPermissionsByRoleIdQuery() { RoleId = roleId }, CancellationToken.None);

            return Ok(response);
        }
    }
}
