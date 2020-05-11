using System;
using System.Collections.Generic;

namespace DataLayer.Repositories
{
    public interface ICrudRepository<T>
    {
        IList<T> Items { get; }
        T CreateOrUpdate(T item);
        T Create(T item);
        T Find(Func<T, bool> predicate);
        T Update(T item);
        void Delete(T item);
        void Delete(Guid id);

    }
}
