using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Bookshop.Dapper.Repositories
{
    internal abstract class BaseRepository
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
