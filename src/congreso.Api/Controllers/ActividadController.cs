using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Actividades;
using congreso.Application.Dtos.Roles;
using congreso.Application.UseCase.Actividades.Queries.GetAll;
using congreso.Application.UseCase.Roles.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> ActividadList([FromQuery] GetAllActividadesQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllActividadesQuery, IEnumerable<ActividadResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
