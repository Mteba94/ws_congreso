using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Dtos.Ponentes;
using congreso.Application.UseCase.NivelesDificultad.Commands.Delete;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetById;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetSelect;
using congreso.Application.UseCase.Ponentes.Commands.Create;
using congreso.Application.UseCase.Ponentes.Commands.Delete;
using congreso.Application.UseCase.Ponentes.Commands.Update;
using congreso.Application.UseCase.Ponentes.Queries.GetAll;
using congreso.Application.UseCase.Ponentes.Queries.GetById;
using congreso.Application.UseCase.Ponentes.Queries.GetPopular;
using congreso.Application.UseCase.Ponentes.Queries.GetSelect;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PonenteController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> PonenteList([FromQuery] GetAllPonenteQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllPonenteQuery, IEnumerable<PonenteResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> PonenteSelect()
        {
            var response = await _dispatcher
              .Dispatch<GetSelectPonenteQuery, IEnumerable<SelectResponseDto>>
              (new GetSelectPonenteQuery(), CancellationToken.None);

            return Ok(response);
        }

        [HttpGet("{ponenteId:int}")]
        public async Task<IActionResult> NivelById(int ponenteId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdPonenteQuery, PonenteByIdResponseDto>(new GetByIdPonenteQuery { PonenteId = ponenteId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePonente([FromBody] CreatePonenteCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreatePonenteCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePonente([FromBody] UpdatePonenteCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdatePonenteCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Delete/{ponenteId:int}")]
        public async Task<IActionResult> DeletePonente(int ponenteId)
        {
            var response = await _dispatcher
                .Dispatch<DeletePonenteCommand, bool>(new DeletePonenteCommand { PonenteId = ponenteId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Popular")]
        public async Task<IActionResult> PonentePopular()
        {
            var response = await _dispatcher
              .Dispatch<GetPopularPonenteQuery, IEnumerable<PonenteResponseDto>>
              (new GetPopularPonenteQuery(), CancellationToken.None);

            return Ok(response);
        }
    }
}
