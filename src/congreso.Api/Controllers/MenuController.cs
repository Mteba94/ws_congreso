using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Menus;
using congreso.Application.UseCase.Menus.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController(IDispatcher dispatcher, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;
        private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

        [HttpGet("MenuByUser")]
        public async Task<IActionResult> GetMenuByUserId()
        {
            var userId = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _dispatcher.Dispatch<GetMenuByUserIdQuery, IEnumerable<MenuResponseDto>>
                (new GetMenuByUserIdQuery() { UserId = int.Parse(userId!) }, CancellationToken.None);

            return Ok(response);
        }
    }
}
