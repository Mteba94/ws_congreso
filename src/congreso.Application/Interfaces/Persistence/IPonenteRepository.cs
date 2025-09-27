using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IPonenteRepository : IGenericRepository<Ponente>
{
    Task<IEnumerable<Ponente>> GetPopularPonentes();
}
