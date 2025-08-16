using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.TiposIdentificacion.Queries.GetAll;
using congreso.Application.UseCase.TiposIdentificacion.Queries.GetById;
using congreso.Application.UseCase.Users.Queries.GetById;
using congreso.Domain.Entities;
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

        [HttpGet("{tipoIdentificacionId:int}")]
        public async Task<IActionResult> TipoIdentificacionDetails(int tipoIdentificacionId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdTipoIdentificacionQuery, TipoIdentificacionByIdResponseDTO>(new GetByIdTipoIdentificacionQuery { TipoIdentificacionId = tipoIdentificacionId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
