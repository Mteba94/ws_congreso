using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Inscripciones;

namespace congreso.Application.UseCase.Inscripciones.Queries.GenerateQrCodeForInscription;

public sealed record GenerateQrCodeForInscriptionQuery(int InscripcionId) : IQuery<GenerateQrCodeResponseDto>;
