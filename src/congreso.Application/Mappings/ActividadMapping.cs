using congreso.Application.Dtos.Actividades;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class ActividadMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Actividad, ActividadResponseDto>()
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
