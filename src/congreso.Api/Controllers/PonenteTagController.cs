using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.Dtos.PonenteTags;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetAll;
using congreso.Application.UseCase.NivelesDificultad.Queries.GetById;
using congreso.Application.UseCase.PonenteTags.Queries.GetAll;
using congreso.Application.UseCase.PonenteTags.Queries.GetById;
using congreso.Application.UseCase.PonenteTags.Queries.GetByIdPonente;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PonenteTagController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;


        [HttpGet]
        public async Task<IActionResult> PonenteTagList([FromQuery] GetAllPonenteTagQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllPonenteTagQuery, IEnumerable<PonenteTagsResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{ponenteTagId:int}")]
        public async Task<IActionResult> PonenteTagById(int ponenteTagId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdPonenteTagQuery, PonenteTagsByIdResponseDto>(new GetByIdPonenteTagQuery { PonenteTagsId = ponenteTagId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ByPonente/{ponenteId:int}")]
        public async Task<IActionResult> PonenteTagByPonenteId(int ponenteId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdPonentePonenteTagQuery, IEnumerable<PonenteTagsByIdResponseDto>>(new GetByIdPonentePonenteTagQuery { PonenteId = ponenteId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
