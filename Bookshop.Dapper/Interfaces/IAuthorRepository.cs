using Bookshop.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookshop.Dapper.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        IEnumerable<Author> GetAuthorsByBookId(Guid bookId);
    }
}
