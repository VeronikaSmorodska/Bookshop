using BookshopPersistenceLayer.Entities;
using System;

namespace BookshopPersistenceLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IRepository<User> Users { get; }
        IAuthorRepository Authors { get; }
        void Save();
    }
}
