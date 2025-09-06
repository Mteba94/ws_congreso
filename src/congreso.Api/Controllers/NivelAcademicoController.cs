using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.UseCase.NivelesAcademicos.Queries.SelectNivelAcademico;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelAcademicoController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("Select")]
        public async Task<IActionResult> NivelAcademicoSelect()
        {
            var response = await _dispatcher
                .Dispatch<SelectNivelAcademicoQuery, IEnumerable<SelectResponseDto>>(new SelectNivelAcademicoQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
