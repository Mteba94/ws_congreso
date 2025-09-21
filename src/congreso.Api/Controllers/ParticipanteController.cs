using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Participantes;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Participantes.Commands.Create;
using congreso.Application.UseCase.Participantes.Commands.Update;
using congreso.Application.UseCase.Participantes.Queries.GetAll;
using congreso.Application.UseCase.Participantes.Queries.GetById;
using congreso.Application.UseCase.Participantes.Queries.GetSelect;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Application.UseCase.Users.Comands.UpdateUser;
using congreso.Application.UseCase.Users.Queries.GetAllUser;
using congreso.Application.UseCase.Users.Queries.GetById;
using congreso.Application.UseCase.Users.Queries.GetSelect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ParticipanteController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [Authorize(Policy = "LISTADO DE PARTICIPANTES")]
        [HttpGet]
        public async Task<IActionResult> ParticipanteList([FromQuery] GetAllParticipanteQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllParticipanteQuery, IEnumerable<ParticipantesResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ParticipanteSelect()
        {
            var response = await _dispatcher
              .Dispatch<GetSelectParticipanteQuery, IEnumerable<SelectResponseDto>>
              (new GetSelectParticipanteQuery(), CancellationToken.None);
            return Ok(response);
        }

        [HttpGet("{participanteId:int}")]
        public async Task<IActionResult> ParticipanteById(int participanteId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdParticipanteQuery, ParticipanteByIdResponseDto>(new GetByIdParticipanteQuery { ParticipanteId = participanteId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateParticipante([FromBody] CreateParticipanteCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateParticipanteCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateParticipante([FromBody] UpdateParticipanteCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateParticipanteCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
