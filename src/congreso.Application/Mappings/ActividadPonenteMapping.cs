using congreso.Application.Dtos.ActividadesPonente;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class ActividadPonenteMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ActividadPonente, ActividadPonenteResponseDto>()
            .Map(dest => dest.ActividadPonenteId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
