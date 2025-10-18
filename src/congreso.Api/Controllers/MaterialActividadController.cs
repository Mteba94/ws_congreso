using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.MaterialesActividad;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.UseCase.MaterialesActividad.Queries.GetAll;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialActividadController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> MaterialActividadList([FromQuery] GetAllMaterialActividadQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllMaterialActividadQuery, IEnumerable<MaterialActividadResposeDTO>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}