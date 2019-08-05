using Bookshop.Dapper.Entities;
using Bookshop.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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
