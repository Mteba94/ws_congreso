using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Participantes;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Participantes.Queries.GetAll;
using congreso.Application.UseCase.Users.Queries.GetAllUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipanteController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> ParticipanteList([FromQuery] GetAllParticipanteQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllParticipanteQuery, IEnumerable<ParticipantesResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
