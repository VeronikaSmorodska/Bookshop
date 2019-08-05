﻿using BookshopPersistenceLayer.Entities;
using BookshopPersistenceLayer.EntityFramework;
using BookshopPersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookshopPersistenceLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private BookshopContext db;
        private BookRepository bookRepository;
        private AuthorRepository authorRepository;
        private UserRepository userRepository;
        //public EFUnitOfWork(string connectionString)
        //{
        //    db = new BookshopContext(connectionString);
        //}

        public UnitOfWork(BookshopContext context)
        {
            db = context;
        }

        public IBookRepository Books
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new BookRepository(db);
                }
                return bookRepository;
            }
        }
        public IAuthorRepository Authors
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new AuthorRepository(db);
                }
                return authorRepository;
            }
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges(); 
        }

        private bool disposed = false;
        private string connection;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
