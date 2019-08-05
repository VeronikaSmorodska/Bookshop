using BookshopPersistenceLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
