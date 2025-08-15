using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface ITipoParticipanteRepository
{
    IQueryable<TipoParticipante> GetAllQueryable();
    Task<IEnumerable<TipoParticipante>> GetAllAsync();
    Task<TipoParticipante> GetByIdAsync(int id);
    Task CreateAsync(TipoParticipante entity);
    void Update(TipoParticipante entity);
    Task DeleteAsync(int id);
}
