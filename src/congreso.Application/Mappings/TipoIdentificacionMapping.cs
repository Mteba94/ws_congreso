using congreso.Application.Dtos.TipoIdentificacion;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class TipoIdentificacionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TipoIdentificacion, TipoIdentificacionResponseDTO>()
            .Map(dest => dest.EstadoDescipcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
