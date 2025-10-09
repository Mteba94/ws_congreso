using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Actividades;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Dtos.Roles;
using congreso.Application.UseCase.Actividades.Commands.Create;
using congreso.Application.UseCase.Actividades.Queries.GetAll;
using congreso.Application.UseCase.Actividades.Queries.GetById;
using congreso.Application.UseCase.NivelesDificultad.Commands.Create;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetById;
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

        [HttpGet("{actividadId:int}")]
        public async Task<IActionResult> ActividadById(int actividadId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdActividadQuery, ActividadByIdResponseDto>(new GetByIdActividadQuery { ActividadId = actividadId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateActividad([FromForm] CreateActividadCommand command)
        {
            var ponente = command.GetPonente();
            var objetivos = command.GetObjetivos();
            var materiales = command.GetMateriales();

            var response = await _dispatcher
                .Dispatch<CreateActividadCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
