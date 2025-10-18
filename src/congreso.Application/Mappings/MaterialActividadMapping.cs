using congreso.Application.Dtos.MaterialesActividad;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class MaterialActividadMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MaterialActividad, MaterialActividadResposeDTO>()
            .Map(dest => dest.MaterialId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
