using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter =null);
        List<T> GetForPageable(Expression<Func<T, bool>> filter = null, int pageIndex = 0, int pageCount = 20);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int GetCount(Expression<Func<T, bool>> filter = null);
    }
}
