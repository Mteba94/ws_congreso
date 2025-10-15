using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Participantes;
using congreso.Application.Dtos.User;
using congreso.Application.Helpers;
using congreso.Application.Interfaces.Services;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Application.UseCase.Users.Comands.DeleteUser;
using congreso.Application.UseCase.Users.Comands.UpdateUser;
using congreso.Application.UseCase.Users.Queries.GetAllUser;
using congreso.Application.UseCase.Users.Queries.GetById;
using congreso.Application.UseCase.Users.Queries.GetSelect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IDispatcher dispatcher, IExcelService excelService, IPdfService pdfService) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;
        private readonly IExcelService _excelService = excelService;
        private readonly IPdfService _pdfService = pdfService;

        //[Authorize(Policy = "LISTADO DE USUARIOS")]
        [HttpGet]
        public async Task<IActionResult> UserList([FromQuery] GetAllUserQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllUserQuery, IEnumerable<ParticipantesResponseDto>>(query, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> UserSelect()
        {
            var response = await _dispatcher
              .Dispatch<GetUserSelectQuery, IEnumerable<SelectResponseDto>>
              (new GetUserSelectQuery(), CancellationToken.None);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> UserById(int userId)
        {
            var response = await _dispatcher
                .Dispatch<GetByIdUserQuery, UserByIdResponseDto>(new GetByIdUserQuery { UserId = userId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await _dispatcher
                .Dispatch<CreateUserCommand, bool>(command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var response = await _dispatcher
                .Dispatch<UpdateUserCommand, bool>(command, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var response = await _dispatcher
                .Dispatch<DeleteUserCommand, bool>(new DeleteUserCommand { UserId = userId }, CancellationToken.None);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Excel")]
        public async Task<IActionResult> UserReportExcel([FromQuery] GetAllUserQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllUserQuery, IEnumerable<ParticipantesResponseDto>>(query, CancellationToken.None);

            var columnNames = ReportColumns.GetColumnsUsers();
            var fileBytes = _excelService.GenerateToExcel(response.Data!, columnNames);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet("Pdf")]
        public async Task<IActionResult> UserReportPdf([FromQuery] GetAllUserQuery query)
        {
            var response = await _dispatcher
                .Dispatch<GetAllUserQuery, IEnumerable<ParticipantesResponseDto>>(query, CancellationToken.None);

            var columnNames = ReportColumns.GetColumnsUsers();
            var fileBytes = _pdfService.GenerateToPdf(response.Data!, columnNames, "Usuarios");
            return File(fileBytes, "application/pdf");
        }
    }
}
