using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.PonenteTags;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class PonenteTagMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PonenteTag, PonenteTagsResponseDto>()
            .Map(dest => dest.PonenteTagId, src => src.Id)
            .Map(dest => dest.EstadoDescription, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<PonenteTag, PonenteTagsByIdResponseDto>()
            .Map(dest => dest.PonenteTagId, src => src.Id)
            .TwoWays();

        config.NewConfig<PonenteTag, SelectResponseDto>()
            .Map(dest => dest.Id, src => src.Id)
            .TwoWays();
    }
}
