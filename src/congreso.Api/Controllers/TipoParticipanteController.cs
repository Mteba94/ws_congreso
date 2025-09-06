using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.TipoParticipante;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.TiposIdentificacion.Queries.GetById;
using congreso.Application.UseCase.TiposParticipante.Commands.Create;
using congreso.Application.UseCase.TiposParticipante.Commands.Update;
using congreso.Application.UseCase.TiposParticipante.Queries.GetAllTipoParticipante;
using congreso.Application.UseCase.TiposParticipante.Queries.GetById;
using congreso.Application.UseCase.TiposParticipante.Queries.GetTipoParticipanteSelect;
using congreso.Application.UseCase.Users.Comands.UpdateUser;
using congreso.Application.UseCase.Users.Queries.GetById;
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

        [HttpGet("{tipoParticipanteId:int}")]
        public async Task<IActionResult> TipoParticipanteById(int tipoParticipanteId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdTipoParticipanteQuery, TipoParticipanteByIdResponseDTO>(new GetByIdTipoParticipanteQuery { tipoParticipanteId = tipoParticipanteId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoParticipante([FromBody] CreateTipoParticipanteCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateTipoParticipanteCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTipoParticipante([FromBody] UpdateTipoParticipanteCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateTipoParticipanteCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
