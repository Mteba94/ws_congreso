using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Congreso;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Congresos.Queries.GetAllCongreso;
using congreso.Application.UseCase.Users.Queries.GetAllUser;
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
    }
}
