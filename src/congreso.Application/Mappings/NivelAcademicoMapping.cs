using congreso.Application.Dtos.Commons;
using congreso.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Application.Mappings
{
    public class NivelAcademicoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<NivelAcademico, SelectResponseDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Nombre, src => src.Nombre)
                .Map(dest => dest.Descripcion, src => src.Descripcion)
                .TwoWays();
        }
    }
}
