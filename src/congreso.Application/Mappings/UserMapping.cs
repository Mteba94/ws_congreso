using congreso.Application.Dtos.User;
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
            .Map(dest => dest.EstadoDescripcion, src => src.Estado == 1 ? "Activo" : "Inactivo")
            .TwoWays();
    }
}
