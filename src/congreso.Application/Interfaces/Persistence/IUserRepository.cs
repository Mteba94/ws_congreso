using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> UserByEmailAsync(string email);
    Task<User> UserByIdentificacion(string identificacion);
}
