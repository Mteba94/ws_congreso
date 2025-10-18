using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Inscripciones;

namespace congreso.Application.UseCase.Inscripciones.Queries.GetByUserId;

public sealed class GetByUserIdInscripcionQuery : IQuery<IEnumerable<InscripcionesByUserDTO>>
{
    public int UserId { get; set; }
}
