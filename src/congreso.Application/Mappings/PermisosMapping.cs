using congreso.Application.Dtos.Permisos;
using congreso.Domain.Entities;
using Mapster;

namespace congreso.Application.Mappings;

public class PermisosMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Permission, PermissionsResponseDto>()
          .Map(dest => dest.PermissionId, src => src.Id)
          .Map(dest => dest.PermissionName, src => src.Nombre)
          .Map(dest => dest.PermissionDescription, src => src.Descripcion)
          .TwoWays();
    }
}
