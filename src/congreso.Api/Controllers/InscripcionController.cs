using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Inscripciones;
using congreso.Application.UseCase.Inscripciones.Commands.Create;
using congreso.Application.UseCase.Inscripciones.Commands.Delete;
using congreso.Application.UseCase.Inscripciones.Commands.GenerateDiploma;
using congreso.Application.UseCase.Inscripciones.Commands.UpdateResult;
using congreso.Application.UseCase.Inscripciones.Queries.GetAll;
using congreso.Application.UseCase.Inscripciones.Queries.GetByUserId;
using congreso.Application.UseCase.Inscripciones.Queries.GetTopWinnersByActividad;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> GetAllInscripciones()
        {
            var response = await _dispatcher
                .Dispatch<GetAllInscripcionesQuery, IEnumerable<InscripcionesResponseDTO>>(new GetAllInscripcionesQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("User/{userId:int}")]
        public async Task<IActionResult> InscriccionByUserId(int userId)
        {
            var response = await _dispatcher
                .Dispatch<GetByUserIdInscripcionQuery, IEnumerable<InscripcionesByUserDTO>>(new GetByUserIdInscripcionQuery { UserId = userId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("TopWinners/{actividadId:int}")]
        public async Task<IActionResult> GetTopWinnersByActividad(int actividadId, [FromQuery] int topN = 3)
        {
            var response = await _dispatcher
                .Dispatch<GetTopWinnersByActividadQuery, IEnumerable<InscripcionesByUserDTO>>(new GetTopWinnersByActividadQuery { ActividadId = actividadId, TopN = topN }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateInscripcion([FromBody] CreateInscripcionCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateInscripcionCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("GenerateDiploma")]
        public async Task<IActionResult> GenerateDiploma([FromBody] GenerateDiplomaCommand command)
        {
            var response = await _dispatcher
                .Dispatch<GenerateDiplomaCommand, string>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("UpdateResult")]
        public async Task<IActionResult> UpdateInscripcionResult([FromBody] UpdateInscripcionResultCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateInscripcionResultCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Delete/{inscripcionId:int}")]
        public async Task<IActionResult> DeleteInscripcion(int inscripcionId)
        {
            var response = await _dispatcher
                .Dispatch<DeleteInscripcionCommand, bool>(new DeleteInscripcionCommand { InscripcionId = inscripcionId }, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
