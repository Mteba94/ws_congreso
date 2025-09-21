using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Ponentes;
using congreso.Application.UseCase.Ponentes.Commands.Create;
using congreso.Application.UseCase.Ponentes.Commands.Delete;
using congreso.Application.UseCase.Ponentes.Commands.Update;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class PonenteMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Ponente, PonenteResponseDto>()
            .Map(dest => dest.PonenteId, src => src.Id)
            .Map(dest => dest.NombrePonente, src => src.Nombre)
            .Map(dest => dest.ApellidoPonente, src => src.Apellido)
            .Map(dest => dest.TituloPonente, src => src.Titulo)
            .Map(dest => dest.EmpresaPonente, src => src.Empresa)
            .Map(dest => dest.BioPonente, src => src.Biografia)
            .Map(dest => dest.imagePonente, src => src.Foto)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<Ponente, PonenteByIdResponseDto>()
            .Map(dest => dest.PonenteId, src => src.Id)
            .Map(dest => dest.NombrePonente, src => src.Nombre)
            .Map(dest => dest.ApellidoPonente, src => src.Apellido)
            .Map(dest => dest.TituloPonente, src => src.Titulo)
            .Map(dest => dest.EmpresaPonente, src => src.Empresa)
            .Map(dest => dest.BioPonente, src => src.Biografia)
            .Map(dest => dest.imagePonente, src => src.Foto)
            .TwoWays();

        config.NewConfig<Ponente, SelectResponseDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Nombre, src => src.Nombre + " " + src.Apellido)
            .Map(dest => dest.Descripcion, src => src.Titulo)
            .TwoWays();

        config.NewConfig<CreatePonenteCommand, Ponente>()
            .Map(dest => dest.Nombre, src => src.NombrePonente)
            .Map(dest => dest.Apellido, src => src.ApellidoPonente)
            .Map(dest => dest.Titulo, src => src.TituloPonente)
            .Map(dest => dest.Empresa, src => src.EmpresaPonente)
            .Map(dest => dest.Biografia, src => src.BioPonente)
            .Map(dest => dest.Foto, src => src.ImagenPonente)
            .TwoWays();

        config.NewConfig<UpdatePonenteCommand, Ponente>()
            .Map(dest => dest.Id, src => src.PonenteId)
            .Map(dest => dest.Nombre, src => src.NombrePonente)
            .Map(dest => dest.Apellido, src => src.ApellidoPonente)
            .Map(dest => dest.Titulo, src => src.TituloPonente)
            .Map(dest => dest.Empresa, src => src.EmpresaPonente)
            .Map(dest => dest.Biografia, src => src.BioPonente)
            .Map(dest => dest.Foto, src => src.ImagenPonente)
            .TwoWays();

        config.NewConfig<DeletePonenteCommand, Ponente>()
            .Map(dest => dest.Id, src => src.PonenteId)
            .TwoWays();
    }
}
