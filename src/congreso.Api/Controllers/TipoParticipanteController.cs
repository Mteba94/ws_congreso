using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.TipoParticipante;
using congreso.Application.UseCase.TiposParticipante.Queries.GetAllTipoParticipante;
using congreso.Application.UseCase.TiposParticipante.Queries.GetTipoParticipanteSelect;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoParticipanteController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> TipoParticipanteList([FromQuery] GetAllTipoParticipanteQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllTipoParticipanteQuery, IEnumerable<TipoParticipanteResponseDTO>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> TipoParticipanteSelect()
        {
            var response = await _dispatcher
                .Dispatch<GetTipoParticipanteSelectQuery, IEnumerable<SelectResponseDto>>(new GetTipoParticipanteSelectQuery(), CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
