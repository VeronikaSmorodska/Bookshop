using BookshopPersistenceLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookshopPersistenceLayer.Interfaces
{
    public interface IBookRepository:IRepository<Book>
    {
        IEnumerable<Book> GetBooksByAuthorId (Guid authorId);
    }
}
