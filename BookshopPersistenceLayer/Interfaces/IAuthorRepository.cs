using BookshopPersistenceLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookshopPersistenceLayer.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        IEnumerable<Author> GetAuthorsByBookId(Guid bookId);
    }
}
