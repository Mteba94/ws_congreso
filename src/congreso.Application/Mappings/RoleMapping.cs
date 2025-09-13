using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.Roles;
using congreso.Application.UseCase.Roles.Commands.Create;
using congreso.Application.UseCase.Roles.Commands.Update;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class RoleMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Role, RoleResponseDto>()
            .Map(dest => dest.RoleId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();

        config.NewConfig<Role, SelectResponseDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Nombre, src => src.Nombre)
            .Map(dest => dest.Descripcion, src => src.Descripcion)
            .TwoWays();

        config.NewConfig<Role, RoleByIdResponseDto>()
            .Map(dest => dest.RoleId, src => src.Id)
            .TwoWays();

        config.NewConfig<CreateRoleCommand, Role>();
        config.NewConfig<UpdateRoleCommand, Role>();

    }
}
