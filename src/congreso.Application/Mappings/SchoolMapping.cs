using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.School;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings
{
    public class SchoolMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<School, SchoolResponseDto>()
                .Map(dest => dest.SchoolId, src => src.Id)
                .Map(dest => dest.SchoolName, src => src.nombre)
                .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
                .TwoWays();

            config.NewConfig<School, SelectResponseDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Nombre, src => src.nombre)
                .TwoWays();

            config.NewConfig<School, SchoolByIdResponseDto>()
                .Map(dest => dest.schoolId, src => src.Id)
                .Map(dest => dest.schoolName, src => src.nombre)
                .TwoWays();
        }
    }
}
