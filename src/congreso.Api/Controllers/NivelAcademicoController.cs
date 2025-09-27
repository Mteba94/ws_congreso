using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.NivelesAcademicos;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.UseCase.NivelesAcademicos.Queries.GetAll;
using congreso.Application.UseCase.NivelesAcademicos.Queries.SelectNivelAcademico;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelAcademicoController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> NivelList([FromQuery] GetAllNivelAcademicoQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllNivelAcademicoQuery, IEnumerable<NivelAcademicoResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> NivelAcademicoSelect()
        {
            var response = await _dispatcher
                .Dispatch<SelectNivelAcademicoQuery, IEnumerable<SelectResponseDto>>(new SelectNivelAcademicoQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
