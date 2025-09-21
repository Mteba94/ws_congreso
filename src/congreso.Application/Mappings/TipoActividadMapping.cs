using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.TiposActividad;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class TipoActividadMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TipoActividad, TipoActividadResponseDto>()
            .Map(dest => dest.TipoActividadId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<TipoActividad, SelectResponseDto>()
               .Map(dest => dest.Id, src => src.Id)
               .Map(dest => dest.Nombre, src => src.Nombre)
               .TwoWays();
    }
}
