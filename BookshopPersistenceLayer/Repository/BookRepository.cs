using BookshopPersistenceLayer.Entities;
using BookshopPersistenceLayer.EntityFramework;
using BookshopPersistenceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BookshopPersistenceLayer.Repository
{
    class BookRepository:IBookRepository
    {
        private BookshopContext db;

        public BookRepository(BookshopContext db)
        {
            this.db = db;
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Delete(Guid id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
            {
                db.Books.Remove(book);
            }
        }

        public Book Get(Guid id)
        {
            return db.Books.Include(a => a.Authorships)
                .ThenInclude(au => au.Author)
                .Where(a => a.BookId == id)
                .FirstOrDefault();
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books.Include(a => a.Authorships)
                           .ThenInclude(au => au.Author);
        }

        public IEnumerable<Book> GetBooksByAuthorId(Guid authorId)
        {
            IEnumerable<Book> booksByAuthor = db.Books.Include(b => b.Authorships).Where(b => b.Authorships.Any(a => a.AuthorId == authorId)).ToList();
            return booksByAuthor;

        }

        public void Update(Book book)
        {
            var enrty = db.Entry(book);
            db.Update(book);
        }

    }
}
