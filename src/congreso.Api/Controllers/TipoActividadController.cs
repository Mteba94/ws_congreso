using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.UseCase.Schools.Queries.SelectSchool;
using congreso.Application.UseCase.TiposActividad.Commands.Create;
using congreso.Application.UseCase.TiposActividad.Queries.Select;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoActividadController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("Select")]
        public async Task<IActionResult> TipoActividadSelect()
        {
            var response = await _dispatcher
                .Dispatch<SelectTipoActividadQuery, IEnumerable<SelectResponseDto>>(new SelectTipoActividadQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoActividad([FromBody] CreateTipoActividadCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateTipoActividadCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
