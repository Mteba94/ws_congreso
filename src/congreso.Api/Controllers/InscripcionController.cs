using congreso.Application.Abstractions.Messaging;
using congreso.Application.UseCase.Inscripciones.Commands.Create;
using congreso.Application.UseCase.NivelesDificultad.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpPost("Create")]
        public async Task<IActionResult> CreateInscripcion([FromBody] CreateInscripcionCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateInscripcionCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
