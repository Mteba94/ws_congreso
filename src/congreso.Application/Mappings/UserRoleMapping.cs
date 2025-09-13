using congreso.Application.Dtos.UserRoles;
using congreso.Application.UseCase.UserRoles.Commands.Create;
using congreso.Application.UseCase.UserRoles.Commands.Update;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;

public class UserRoleMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RoleUsuario, UserRoleResponseDto>()
          .Map(dest => dest.UserRoleId, src => src.Id)
          .Map(dest => dest.User, src => src.User.Pnombre + " " + src.User.Papellido)
          .Map(dest => dest.Role, src => src.Role.Nombre)
          .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
          .TwoWays();

        config.NewConfig<RoleUsuario, UserRoleByIdResponseDto>()
          .Map(dest => dest.UserRoleId, src => src.Id)
          .Map(dest => dest.State, src => src.Estado)
          .TwoWays();

        config.NewConfig<CreateUserRoleCommand, RoleUsuario>();

        config.NewConfig<UpdateUserRoleCommand, RoleUsuario>()
            .Map(dest => dest.Estado, src => src.State)
            .TwoWays();
    }
}
