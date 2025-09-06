using congreso.Application.Dtos.Commons;
using congreso.Domain.Entities;
using Mapster;

namespace congreso.Application.Mappings;

public class TipoActividadMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TipoActividad, SelectResponseDto>()
               .Map(dest => dest.Id, src => src.Id)
               .Map(dest => dest.Nombre, src => src.Nombre)
               .TwoWays();
    }
}
