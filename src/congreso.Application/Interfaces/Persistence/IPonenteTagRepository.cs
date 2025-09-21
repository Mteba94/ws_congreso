using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence;

public interface IPonenteTagRepository : ICommonRepository<PonenteTag>
{
    Task<IEnumerable<PonenteTag>> GetTagPonentesByPonenteId(int ponenteId);
    Task<bool> RegistrarPonenteTags(IEnumerable<PonenteTag> ponenteTags);
    Task<bool> EliminarPonenteTags(IEnumerable<PonenteTag> ponenteTags);
    Task<bool> EliminarPonenteTagsByPonenteId(int ponenteId);
}
