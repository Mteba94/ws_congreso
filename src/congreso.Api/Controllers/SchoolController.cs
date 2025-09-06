using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.School;
using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Schools.Queries.GetById;
using congreso.Application.UseCase.Schools.Queries.SelectSchool;
using congreso.Application.UseCase.TiposIdentificacion.Queries.GetAll;
using congreso.Application.UseCase.TiposParticipante.Queries.GetTipoParticipanteSelect;
using congreso.Application.UseCase.Users.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("Select")]
        public async Task<IActionResult> SchoolSelect()
        {
            var response = await _dispatcher
                .Dispatch<SelectSchoolQuery, IEnumerable<SelectResponseDto>>(new SelectSchoolQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{schoolId:int}")]
        public async Task<IActionResult> SchoolById(int schoolId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdSchoolQuery, SchoolByIdResponseDto>(new GetByIdSchoolQuery { schoolId = schoolId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
