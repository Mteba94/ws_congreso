using congreso.Application.Abstractions.Messaging;
using congreso.Application.Commons.Bases;
using congreso.Application.Dtos.TipoIdentificacion;

namespace congreso.Application.UseCase.TiposIdentificacion.Queries.GetAll;

public sealed class GetAllTipoIdentificacionQuery : BaseFilters, IQuery<IEnumerable<TipoIdentificacionResponseDTO>>
{

}
