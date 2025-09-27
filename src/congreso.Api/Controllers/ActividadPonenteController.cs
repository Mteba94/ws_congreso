using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.ActividadesPonente;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.UseCase.ActividadesPonente.Queries.GetAll;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadPonenteController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> ActividadPonenteList([FromQuery] GetAllActividadPonenteQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllActividadPonenteQuery, IEnumerable<ActividadPonenteResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
