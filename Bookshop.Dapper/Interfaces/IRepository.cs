using System;
using System.Collections.Generic;

namespace Bookshop.Dapper.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        T Create(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}
