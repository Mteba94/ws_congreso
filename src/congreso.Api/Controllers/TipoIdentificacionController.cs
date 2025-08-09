using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.UseCase.TiposIdentificacion.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIdentificacionController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> TipoIdentificacionList([FromQuery] GetAllTipoIdentificacionQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllTipoIdentificacionQuery, IEnumerable<TipoIdentificacionResponseDTO>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
