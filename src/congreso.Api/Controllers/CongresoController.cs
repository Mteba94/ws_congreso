using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Congreso;
using congreso.Application.UseCase.Congresos.Commands.Create;
using congreso.Application.UseCase.Congresos.Commands.Update;
using congreso.Application.UseCase.Congresos.Queries.GetAllCongreso;
using congreso.Application.UseCase.Congresos.Queries.GetById;
using congreso.Application.UseCase.Congresos.Queries.GetSelect;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Application.UseCase.Users.Comands.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongresoController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> CongresoList([FromQuery] GetAllCongresoQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllCongresoQuery, IEnumerable<CongresoResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> CongresoSelect()
        {
            var response = await _dispatcher
              .Dispatch<GetSelectCongresoQuery, IEnumerable<SelectResponseDto>>
              (new GetSelectCongresoQuery(), CancellationToken.None);
            return Ok(response);
        }

        [HttpGet("{congresoId:int}")]
        public async Task<IActionResult> CongresoById(int congresoId)
        {
            var response = await _dispatcher
                .Dispatch<GetCongresoByIdQuery, CongresoByIdResponseDto>(new GetCongresoByIdQuery { CongresoId = congresoId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCongreso([FromBody] CreateCongresoCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateCongresoCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCongreso([FromBody] UpdateCongresoCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateCongresoCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
