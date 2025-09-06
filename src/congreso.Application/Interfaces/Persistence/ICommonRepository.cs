using congreso.Domain.Entities;

namespace congreso.Application.Interfaces.Persistence
{
    public interface ICommonRepository<T> where T : CatalogoEntity
    {
        IQueryable<T> GetAllQueryable();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
    }
}
