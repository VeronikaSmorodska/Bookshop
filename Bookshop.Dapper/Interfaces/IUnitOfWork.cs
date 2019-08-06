using Bookshop.Dapper.Entities;
using System;

namespace Bookshop.Dapper.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IRepository<User> Users { get; }
        IAuthorRepository Authors { get; }
        void Save();
    }
}
