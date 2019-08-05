using BookshopPersistenceLayer.Entities;
using BookshopPersistenceLayer.EntityFramework;
using BookshopPersistenceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookshopPersistenceLayer.Repository
{
    class UserRepository : IRepository<User>
    {
        private BookshopContext db;

        public UserRepository(BookshopContext db)
        {
            this.db = db;
        }

        public void Create(User user)
        {
            
            db.Users.Add(user);
        }

        public void Delete(Guid id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        public User Get(Guid id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

    }
}
