using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Actividades;

namespace congreso.Application.UseCase.Actividades.Queries.GetById;

public sealed class GetByIdActividadQuery : IQuery<ActividadByIdResponseDto>
{
    public int ActividadId { get; set; }
}
