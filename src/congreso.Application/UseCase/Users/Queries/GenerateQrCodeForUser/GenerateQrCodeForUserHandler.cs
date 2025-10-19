using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.User;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using System.Text;

namespace congreso.Application.UseCase.Users.Queries.GenerateQrCodeForUser;

internal sealed class GenerateQrCodeForUserHandler(IUnitOfWork unitOfWork, IQrCodeService qrCodeService, HandlerExecutor executor) : IQueryHandler<GenerateQrCodeForUserQuery, GenerateUserQrCodeResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQrCodeService _qrCodeService = qrCodeService;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<GenerateUserQrCodeResponseDto>> Handle(GenerateQrCodeForUserQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GenerateQrCodeForUserAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<GenerateUserQrCodeResponseDto>> GenerateQrCodeForUserAsync(GenerateQrCodeForUserQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<GenerateUserQrCodeResponseDto>();

        try
        {
            var user = await _unitOfWork.User.GetByIdAsync(query.UserId);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Usuario no encontrado.";
                return response;
            }

            // The content to be encoded in the QR code
            // Now encoding user ID and email

            string baseUrl = "https://techcongress.azurewebsites.net/api/Asistencia/MarkAttendance";

            // ¡AQUÍ ESTÁ EL CAMBIO!
            // Construimos la URL completa con los datos como query parameters.
            // Usamos Uri.EscapeDataString para asegurar que el email se codifique correctamente (ej. @ se vuelve %40).
            string qrCodeContent = $"{baseUrl}?actividadId={query.actividadId}&email={Uri.EscapeDataString(user.Email)}";

            // --- QR Code Generation Logic using existing IQrCodeService ---
            string qrCodeBase64 = await _qrCodeService.GenerateQrCodeAsBase64Async(qrCodeContent);

            response.IsSuccess = true;
            response.Data = new GenerateUserQrCodeResponseDto
            {
                UserId = user.Id,
                QrCodeContent = qrCodeContent,
                QrCodeBase64Image = qrCodeBase64
            };
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
