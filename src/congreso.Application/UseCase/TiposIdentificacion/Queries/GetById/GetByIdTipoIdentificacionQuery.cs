using congreso.Application.Abstractions.Messaging;
using congreso.Application.Dtos.TipoIdentificacion;

namespace congreso.Application.UseCase.TiposIdentificacion.Queries.GetById;

public sealed class GetByIdTipoIdentificacionQuery : IQuery<TipoIdentificacionByIdResponseDTO>
{
    public int TipoIdentificacionId { get; set; }
}
