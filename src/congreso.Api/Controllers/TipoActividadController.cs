using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.TiposActividad;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Actividades.Queries.GetAll;
using congreso.Application.UseCase.Schools.Queries.SelectSchool;
using congreso.Application.UseCase.TiposActividad.Commands.Create;
using congreso.Application.UseCase.TiposActividad.Queries.GetAll;
using congreso.Application.UseCase.TiposActividad.Queries.Select;
using congreso.Application.UseCase.Users.Queries.GetAllUser;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoActividadController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> TipoActividadList([FromQuery] GetAllTipoActividadQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllTipoActividadQuery, IEnumerable<TipoActividadResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> TipoActividadSelect()
        {
            var response = await _dispatcher
                .Dispatch<SelectTipoActividadQuery, IEnumerable<SelectResponseDto>>(new SelectTipoActividadQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTipoActividad([FromBody] CreateTipoActividadCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateTipoActividadCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
