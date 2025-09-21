using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.NivelesDificultad.Commands.Create;
using congreso.Application.UseCase.NivelesDificultad.Commands.Delete;
using congreso.Application.UseCase.NivelesDificultad.Commands.Update;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetById;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetSelect;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Application.UseCase.Users.Comands.DeleteUser;
using congreso.Application.UseCase.Users.Comands.UpdateUser;
using congreso.Application.UseCase.Users.Queries.GetAllUser;
using congreso.Application.UseCase.Users.Queries.GetById;
using congreso.Application.UseCase.Users.Queries.GetSelect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelDificultadController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> NivelList([FromQuery] GetAllNivelDificultadQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllNivelDificultadQuery, IEnumerable<NivelDificultdadResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> NivelSelect()
        {
            var response = await _dispatcher
              .Dispatch<GetSelectNivelDificultdadQuery, IEnumerable<SelectResponseDto>>
              (new GetSelectNivelDificultdadQuery(), CancellationToken.None);
            return Ok(response);
        }

        [HttpGet("{nivelId:int}")]
        public async Task<IActionResult> NivelById(int nivelId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdNivelDificultadQuery, NivelDificultdadByIdResponseDto>(new GetByIdNivelDificultadQuery { NivelId = nivelId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNivel([FromBody] CreateNivelDificultadCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateNivelDificultadCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateNivel([FromBody] UpdateNivelCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateNivelCommand, bool>(command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Delete/{nivelId:int}")]
        public async Task<IActionResult> DeleteNivel(int nivelId)
        {
            var response = await _dispatcher
                .Dispatch<DeleteNivelDificultadCommand, bool>(new DeleteNivelDificultadCommand { NivelId = nivelId }, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
