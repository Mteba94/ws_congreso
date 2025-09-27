using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.NivelesAcademicos;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
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
            config.NewConfig<NivelAcademico, NivelAcademicoResponseDto>()
                .Map(dest => dest.NivelId, src => src.Id)
                .Map(dest => dest.nombreNivel, src => src.Nombre)
                .Map(dest => dest.descripcionNivel, src => src.Descripcion)
                .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
                .TwoWays();

            config.NewConfig<NivelAcademico, SelectResponseDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Nombre, src => src.Nombre)
                .Map(dest => dest.Descripcion, src => src.Descripcion)
                .TwoWays();
        }
    }
}
