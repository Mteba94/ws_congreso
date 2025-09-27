using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Dtos.ObjetivosActividad;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;
using congreso.Application.UseCase.ObjetivosActividad.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetivoActividadController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> ObjetivosList([FromQuery] GetAllObjetivoActividadQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllObjetivoActividadQuery, IEnumerable<ObjetivosActividadResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
