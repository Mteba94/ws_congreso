using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.Inscripciones;
using congreso.Application.Interfaces.Services;
using congreso.Utilities.Static;
using System.Text;

namespace congreso.Application.UseCase.Inscripciones.Queries.GenerateQrCodeForInscription;

internal sealed class GenerateQrCodeForInscriptionHandler(IUnitOfWork unitOfWork, IQrCodeService qrCodeService, HandlerExecutor executor) : IQueryHandler<GenerateQrCodeForInscriptionQuery, GenerateQrCodeResponseDto>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IQrCodeService _qrCodeService = qrCodeService;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<GenerateQrCodeResponseDto>> Handle(GenerateQrCodeForInscriptionQuery query, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(query, () => GenerateQrCodeForInscriptionAsync(query, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<GenerateQrCodeResponseDto>> GenerateQrCodeForInscriptionAsync(GenerateQrCodeForInscriptionQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<GenerateQrCodeResponseDto>();

        try
        {
            var inscripcion = await _unitOfWork.Inscripcion.GetByIdAsync(query.InscripcionId);

            if (inscripcion == null)
            {
                response.IsSuccess = false;
                response.Message = "Inscripción no encontrada.";
                return response;
            }

            // The content to be encoded in the QR code
            string qrCodeContent = $"inscription:{inscripcion.Id}";

            // --- QR Code Generation Logic using existing IQrCodeService ---
            string qrCodeBase64 = await _qrCodeService.GenerateQrCodeAsBase64Async(qrCodeContent);

            response.IsSuccess = true;
            response.Data = new GenerateQrCodeResponseDto
            {
                InscripcionId = inscripcion.Id,
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
