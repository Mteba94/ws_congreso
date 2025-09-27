using congreso.Application.Dtos.ObjetivosActividad;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class ObjetivoActividadMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ObjetivoActividad, ObjetivosActividadResponseDto>()
            .Map(dest => dest.ObjetivoId, src => src.Id)
            .Map(dest => dest.ObjetivoDescripcion, src => src.ObjetivoDesc)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
