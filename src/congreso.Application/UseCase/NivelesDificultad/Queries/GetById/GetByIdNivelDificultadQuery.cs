using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.NivelesDificultad;

namespace congreso.Application.UseCase.NivelesDificultad.Queries.GetById;

public sealed class GetByIdNivelDificultadQuery : IQuery<NivelDificultdadByIdResponseDto>
{
    public int NivelId { get; set; }
}
