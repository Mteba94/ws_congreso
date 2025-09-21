using congreso.Application.Dtos.Commons;
using congreso.Application.Dtos.User;
using congreso.Application.UseCase.Users.Comands.CreateUser;
using congreso.Domain.Entities;
using congreso.Utilities.Static;
using Mapster;

namespace congreso.Application.Mappings;
public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<User, UserResponseDto>()
            .Map(dest => dest.UserId, src => src.Id)
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == (int)TipoEstado.Activo ? "Activo" : "Inactivo")
            .TwoWays();


        config.NewConfig<User, UserByIdResponseDto>()
            .Map(dest => dest.UserId, src => src.Id)
            .TwoWays();

        config.NewConfig<CreateUserCommand, User>();

        config.NewConfig<User, SelectResponseDto>()
          .Map(dest => dest.Id, src => src.Id)
          .Map(dest => dest.Nombre, src => src.Pnombre + " " + src.Snombre + " " + src.Papellido + " " + src.Sapellido)
          .TwoWays();
    }


}
