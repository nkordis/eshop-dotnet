using System.Diagnostics;
using System.Linq.Expressions;

namespace EShop.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);
    T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}

