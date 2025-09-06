using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.UseCase.CodigosVerificacion.Commands.CreateCodigo;
using congreso.Application.UseCase.CodigosVerificacion.Queries.ValidacionCodigo;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodigoController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpPost]
        public async Task<IActionResult> CreateCodigo([FromBody] CreateCodigoCommand command, CancellationToken cancellationToken)
        {
            var response = await _dispatcher
                .Dispatch<CreateCodigoCommand, string>(command, cancellationToken);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("validar")]
        public async Task<IActionResult> ValidarCodigo([FromBody] ValidacionCodigoQuery query, CancellationToken cancellationToken)
        {
            var response = await _dispatcher
                .Dispatch<ValidacionCodigoQuery, bool>(query, cancellationToken);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
