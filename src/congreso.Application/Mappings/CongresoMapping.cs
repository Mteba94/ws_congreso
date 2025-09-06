using congreso.Application.Dtos.Congreso;
using congreso.Application.Dtos.User;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class CongresoMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Congreso, CongresoResponseDto>()
            .Map(dest => dest.congresoId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
