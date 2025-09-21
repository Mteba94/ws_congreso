using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Tags;
using congreso.Application.UseCase.NivelesDificultad.Commands.Delete;
using congreso.Application.UseCase.NivelesDificultad.Commands.Update;
using congreso.Application.UseCase.Tags.Commands.Create;
using congreso.Application.UseCase.Tags.Commands.Delete;
using congreso.Application.UseCase.Tags.Commands.Update;
using congreso.Application.UseCase.Tags.Queries.GetAll;
using congreso.Application.UseCase.Tags.Queries.GetById;
using congreso.Application.UseCase.Tags.Queries.GetSelect;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> TagList([FromQuery] GetAllTagQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllTagQuery, IEnumerable<TagsResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> TagSelect()
        {
            var response = await _dispatcher
              .Dispatch<GetSelectTagQuery, IEnumerable<SelectResponseDto>>
              (new GetSelectTagQuery(), CancellationToken.None);
            return Ok(response);
        }

        [HttpGet("{tagId:int}")]
        public async Task<IActionResult> TagById(int tagId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdTagQuery, TagByIdResponseDto>(new GetByIdTagQuery { TagId = tagId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateTagCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateTagCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Delete/{tagId:int}")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            var response = await _dispatcher
                .Dispatch<DeleteTagCommand, bool>(new DeleteTagCommand { TagId = tagId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
