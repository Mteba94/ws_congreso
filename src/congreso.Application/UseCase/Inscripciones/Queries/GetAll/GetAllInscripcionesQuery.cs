using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.Inscripciones;

namespace congreso.Application.UseCase.Inscripciones.Queries.GetAll;

public sealed class GetAllInscripcionesQuery : IQuery<IEnumerable<InscripcionesResponseDTO>>
{
    // No specific parameters needed for getting all inscriptions, 
    // but could add pagination, filtering, or sorting parameters here if desired.
}