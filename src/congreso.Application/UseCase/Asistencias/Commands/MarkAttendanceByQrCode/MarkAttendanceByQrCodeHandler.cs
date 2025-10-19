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
            var match = Regex.Match(command.QrCodeContent, @"^user:(\d+),email:([^@]+@[^\.]+\.[^\.]+)$");
            if (!match.Success || !int.TryParse(match.Groups[1].Value, out int userId) || string.IsNullOrWhiteSpace(match.Groups[2].Value))
            {
                response.IsSuccess = false;
                response.Message = "Contenido de QR inválido o incompleto.";
                return response;
            }
            string userEmailFromQr = match.Groups[2].Value;

            // Retrieve User
            var user = await _unitOfWork.User.GetByIdAsync(userId);
            if (user == null || user.Email != userEmailFromQr)
            {
                response.IsSuccess = false;
                response.Message = "Usuario no encontrado o correo electrónico no coincide.";
                return response;
            }

            // Retrieve Inscription for the specific user and activity
            var inscripcion = (await _unitOfWork.Inscripcion.GetAllAsync())
                                .FirstOrDefault(i => i.UserId == userId && i.ActividadId == command.ActividadId);

            if (inscripcion == null)
            {
                response.IsSuccess = false;
                response.Message = "Inscripción no encontrada para este usuario y actividad.";
                return response;
            }

            // Dispatch the existing MarkAttendanceCommand
            var markAttendanceCommand = new MarkAttendanceCommand
            {
                ActividadId = command.ActividadId, // ActividadId now comes from the command
                Email = user.Email // Email comes from the fetched user
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
