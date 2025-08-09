using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface ITipoIdentificacionRepository
{
    IQueryable<TipoIdentificacion> GetAllQueryable();
    Task<IEnumerable<TipoIdentificacion>> GetAllAsync();
    Task<TipoIdentificacion> GetByIdAsync(int id);
    Task CreateAsync(TipoIdentificacion entity);
    void Update(TipoIdentificacion entity);
    Task DeleteAsync(int id);

}
