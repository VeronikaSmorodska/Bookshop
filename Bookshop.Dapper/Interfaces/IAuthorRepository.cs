using Bookshop.Dapper.Entities;
using System;
using System.Collections.Generic;

namespace Bookshop.Dapper.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        IEnumerable<Author> GetAuthorsByBookId(Guid bookId);
    }
}
