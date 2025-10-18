using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Inscripciones;

namespace congreso.Application.UseCase.Inscripciones.Queries.GetTopWinnersByActividad;

public sealed class GetTopWinnersByActividadQuery : IQuery<IEnumerable<InscripcionesByUserDTO>>
{
    public int ActividadId { get; set; }
    public int TopN { get; set; } = 3; // Default to top 3 winners
}