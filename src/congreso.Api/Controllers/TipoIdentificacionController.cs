using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.UseCase.NivelesAcademicos.Queries.SelectNivelAcademico;
using congreso.Application.UseCase.TiposIdentificacion.Commands.CreateTipoIdent;
using congreso.Application.UseCase.TiposIdentificacion.Commands.UpdateTipoIdent;
using congreso.Application.UseCase.TiposIdentificacion.Queries.GetAll;
using congreso.Application.UseCase.TiposIdentificacion.Queries.GetById;
using congreso.Application.UseCase.TiposIdentificacion.Queries.Select;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Application.UseCase.Users.Comands.UpdateUser;
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

        [HttpGet("Select")]
        public async Task<IActionResult> TipoIdentificacionSelect()
        {
            var response = await _dispatcher
                .Dispatch<SelectTipoIdentificacionQuery, IEnumerable<SelectResponseDto>>(new SelectTipoIdentificacionQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{tipoIdentificacionId:int}")]
        public async Task<IActionResult> TipoIdentificacionDetails(int tipoIdentificacionId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdTipoIdentificacionQuery, TipoIdentificacionByIdResponseDTO>(new GetByIdTipoIdentificacionQuery { TipoIdentificacionId = tipoIdentificacionId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoIdentificacion([FromBody] CreateTipoIdentCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateTipoIdentCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTipoIdentificacion([FromBody] UpdateTipoIdentCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateTipoIdentCommand, bool>(command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
