using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.School;
using congreso.Domain.Entities;
using Mapster;

namespace congreso.Application.Mappings
{
    public class SchoolMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
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
