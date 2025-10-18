using congreso.Application.Abstractions.Messaging;
using congreso.Application.UseCase.Asistencias.Commands.MarkAttendance;
using congreso.Application.UseCase.Asistencias.Commands.MarkAttendanceByQrCode;
using congreso.Application.Dtos.Asistencias;
using congreso.Application.UseCase.Asistencias.Queries.GetAllAttendanceDetails;
using congreso.Application.UseCase.Inscripciones.Queries.GenerateAttendanceQrCode;
using Microsoft.AspNetCore.Mvc;

namespace congreso.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("GenerateAttendanceQrCode")]
        public async Task<IActionResult> GenerateAttendanceQrCode([FromQuery] int actividadId)
        {
            var response = await _dispatcher
                .Dispatch<GenerateAttendanceQrCodeQuery, string>(new GenerateAttendanceQrCodeQuery { ActividadId = actividadId }, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("MarkAttendance")]
        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceCommand command)
        {
            var response = await _dispatcher
                .Dispatch<MarkAttendanceCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPost("MarkByQrCode")]
        public async Task<IActionResult> MarkByQrCode([FromBody] MarkAttendanceByQrCodeCommand command)
        {
            var response = await _dispatcher
                .Dispatch<MarkAttendanceByQrCodeCommand, bool>(command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetAllAttendanceDetails()
        {
            var response = await _dispatcher
                .Dispatch<GetAllAttendanceDetailsQuery, IEnumerable<AttendanceDetailDto>>(new GetAllAttendanceDetailsQuery(), CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
