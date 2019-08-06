using Bookshop.Dapper.Entities;
using System;
using System.Collections.Generic;

namespace Bookshop.Dapper.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetBooksByAuthorId(Guid authorId);
    }
}
