using congreso.Application.Commons.Bases;

namespace congreso.Application.Interfaces.Services
{
    public interface IOrderingQuery
    {
        IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryable) where T : class;
    }
}
