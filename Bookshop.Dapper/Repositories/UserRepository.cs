using Bookshop.Dapper.Entities;
using Bookshop.Dapper.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bookshop.Dapper.Repositories
{
    class UserRepository : BaseRepository, IRepository<User>
    {
        public UserRepository(IDbTransaction transaction)
                            : base(transaction)
        {
        }
        public IEnumerable<User> GetAll()
        {
            List<User> users = Connection.Query<User>("SELECT * FROM Users", transaction: Transaction)
                      .ToList();
            return users;
        }

        public User Get(Guid id)
        {
            User user = Connection.Query<User>("SELECT * FROM Users WHERE UserId = @id", new { id }, transaction: Transaction)
                     .FirstOrDefault();
            return user;
        }

        public User Create(User user)
        {
            var sqlQuery = "INSERT INTO Users (UserId, Name, Surname, Login, Password) VALUES(@UserId, @Name, @Surname, @Login, @Password)";
            Guid userId = Connection.Query<Guid>(sqlQuery, user, transaction: Transaction)
                            .FirstOrDefault();
            user.UserId = userId;
            return user;
        }

        public void Update(User user)
        {
            var sqlQuery = "UPDATE Users SET Name = @Name, Surname = @Surname, Login = @Login, Password=@Password WHERE UserId = @UserId";
            Connection.Execute(sqlQuery, user, transaction: Transaction);
        }

        public void Delete(Guid id)
        {
            var user = Connection.Query<User>("SELECT * FROM Users WHERE UserId = @id", new { id }, transaction: Transaction)
                     .FirstOrDefault();
            if (user != null)
            {
                var sqlQuery = "DELETE FROM Users WHERE UserId = @id";
                Connection.Execute(sqlQuery, new { id }, transaction: Transaction);
            }
        }

    }
}
