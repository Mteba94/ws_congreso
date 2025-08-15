using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.TipoParticipante;
using congreso.Application.UseCase.TiposParticipante.Queries.GetAllTipoParticipante;
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
    }
}
