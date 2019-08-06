using BookshopPersistenceLayer.Entities;
using BookshopPersistenceLayer.EntityFramework;
using BookshopPersistenceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BookshopPersistenceLayer.Repository
{
    class UserRepository : IRepository<User>
    {
        private readonly BookshopContext db;
        public UserRepository(BookshopContext db)
        {
            this.db = db;
        }
        public void Create(User user)
        {
            
            db.Users.Add(user);
        }
        public User Delete(Guid id)
        {
            User user = db.Users.Find(id);

            if (user != null)
            {
                db.Users.Remove(user);
            }
            return user;
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
