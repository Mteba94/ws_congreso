using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Application.UseCase.Asistencias.Commands.MarkAttendance;
using congreso.Utilities.Static;
using System.Text.RegularExpressions;

namespace congreso.Application.UseCase.Asistencias.Commands.MarkAttendanceByQrCode;

internal sealed class MarkAttendanceByQrCodeHandler(IDispatcher dispatcher, IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<MarkAttendanceByQrCodeCommand, bool>
{
    private readonly IDispatcher _dispatcher = dispatcher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(MarkAttendanceByQrCodeCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => MarkAttendanceByQrCodeAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> MarkAttendanceByQrCodeAsync(MarkAttendanceByQrCodeCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            // Parse QR Code Content
            var match = Regex.Match(command.QrCodeContent, @"^inscription:(\d+)$");
            if (!match.Success || !int.TryParse(match.Groups[1].Value, out int inscriptionId))
            {
                response.IsSuccess = false;
                response.Message = "Contenido de QR inválido.";
                return response;
            }

            // Retrieve Inscription (with User)
            var inscripcion = await _unitOfWork.Inscripcion.GetByIdAsync(inscriptionId);
            if (inscripcion == null)
            {
                response.IsSuccess = false;
                response.Message = "Inscripción no encontrada.";
                return response;
            }

            // Ensure User is loaded
            if (inscripcion.User == null)
            {
                inscripcion.User = await _unitOfWork.User.GetByIdAsync(inscripcion.UserId);
            }

            if (inscripcion.User == null)
            {
                response.IsSuccess = false;
                response.Message = "No se pudo obtener la información del usuario para la inscripción.";
                return response;
            }

            // Dispatch the existing MarkAttendanceCommand
            var markAttendanceCommand = new MarkAttendanceCommand
            {
                ActividadId = inscripcion.ActividadId, // Get ActividadId from inscription
                Email = inscripcion.User.Email // Get User Email from inscription
            };

            var markAttendanceResponse = await _dispatcher.Dispatch<MarkAttendanceCommand, bool>(markAttendanceCommand, cancellationToken);

            response.IsSuccess = markAttendanceResponse.IsSuccess;
            response.Message = markAttendanceResponse.Message;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
