using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.NivelesDificultad;
using congreso.Application.UseCase.NivelesDificultad.Commands.Create;
using congreso.Application.UseCase.NivelesDificultad.Commands.Update;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class NivelDificultadMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<NivelDificultad, NivelDificultdadResponseDto>()
            .Map(dest => dest.NivelId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<NivelDificultad, NivelDificultdadByIdResponseDto>()
            .Map(dest => dest.NivelId, src => src.Id)
            .TwoWays();

        config.NewConfig<NivelDificultad, SelectResponseDto>()
            .Map(dest => dest.Id, src => src.Id)
            .TwoWays();

        config.NewConfig<CreateNivelDificultadCommand, NivelDificultad>();

        config.NewConfig<UpdateNivelCommand, NivelDificultad>()
            .Map(dest => dest.Id, src => src.nivelId)
            .TwoWays();
    }
}
