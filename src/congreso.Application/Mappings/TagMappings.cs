using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Tags;
using congreso.Application.UseCase.Tags.Commands.Create;
using congreso.Application.UseCase.Tags.Commands.Update;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class TagMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Tag, TagsResponseDto>()
            .Map(dest => dest.TagId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<Tag, TagByIdResponseDto>()
            .Map(dest => dest.TagId, src => src.Id)
            .TwoWays();

        config.NewConfig<Tag, SelectResponseDto>()
            .Map(dest => dest.Id, src => src.Id)
            .TwoWays();

        config.NewConfig<CreateTagCommand, Tag>()
            .Map(dest => dest.Nombre, src => src.nombreTag)
            .TwoWays();

        config.NewConfig<UpdateTagCommand, Tag>()
            .Map(dest => dest.Id, src => src.TagId)
            .Map(dest => dest.Nombre, src => src.NombreTag)
            .TwoWays();
    }
}
