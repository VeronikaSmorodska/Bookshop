using Bookshop.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookshop.Dapper.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetBooksByAuthorId(Guid authorId);
    }
}
