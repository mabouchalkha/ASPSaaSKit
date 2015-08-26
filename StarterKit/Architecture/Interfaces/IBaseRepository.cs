using System.Collections.Generic;

namespace StarterKit.Architecture.Interfaces
{
    public interface IBaseRepository<T, TKey>
    {
        List<T> Index();
        T Read(TKey id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(TKey id);
    }
}