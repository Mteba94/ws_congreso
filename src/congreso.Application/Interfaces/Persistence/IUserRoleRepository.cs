using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IUserRoleRepository : ICommonRepository<RoleUsuario>
{
    Task<RoleUsuario> GetByUserId(int userId);
}
