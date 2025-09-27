using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Dtos.School;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;
using congreso.Application.UseCase.Schools.Queries.GetAll;
using congreso.Application.UseCase.Schools.Queries.GetById;
using congreso.Application.UseCase.Schools.Queries.SelectSchool;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> SchoolList([FromQuery] GetAllSchoolQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllSchoolQuery, IEnumerable<SchoolResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

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
