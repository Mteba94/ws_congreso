using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using System.Text.Json;

namespace congreso.Application.UseCase.Inscripciones.Queries.GenerateAttendanceQrCode;

internal sealed class GenerateAttendanceQrCodeHandler(IUnitOfWork unitOfWork, IQrCodeService qrCodeService, HandlerExecutor executor) : IQueryHandler<GenerateAttendanceQrCodeQuery, string>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQrCodeService _qrCodeService = qrCodeService;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<string>> Handle(GenerateAttendanceQrCodeQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GenerateQrCodeAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<string>> GenerateQrCodeAsync(GenerateAttendanceQrCodeQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            // 1. Validate Actividad exists
            var actividad = await _unitOfWork.Actividad.GetByIdAsync(query.ActividadId);
            if (actividad == null)
            {
                response.IsSuccess = false;
                response.Message = "Actividad no válida.";
                return response;
            }

            // 2. Construct the URL to embed in QR code
            //string attendanceBaseUrl = _configuration["FrontendSettings:AttendanceBaseUrl"] ?? throw new InvalidOperationException("FrontendSettings:AttendanceBaseUrl no configurado.");
            
            // Example URL: https://yourfrontend.com/attendance?actividadId=123&timestamp=... 
            // The frontend will be responsible for parsing this URL and marking attendance.
            string qrCodeUrl = $"{"url"}?actividadId={query.ActividadId}&timestamp={DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";

            // 3. Generate QR code as Base64 string
            string qrCodeBase64 = await _qrCodeService.GenerateQrCodeAsBase64Async(qrCodeUrl);

            response.IsSuccess = true;
            response.Data = qrCodeBase64;
            response.Message = "Código QR generado exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}