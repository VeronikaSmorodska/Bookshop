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
    public class AuthorRepository : IAuthorRepository
    {
        private BookshopContext db;

        public AuthorRepository(BookshopContext db)
        {
            this.db = db;
        }

        public void Create(Author author)
        {
           
            db.Authors.Add(author);
        }

        public void Delete(Guid id)
        {
            Author author = db.Authors.Find(id);
            if (author != null)
            {
                db.Authors.Remove(author);
            }
        }

        public Author Get(Guid id)
        {
            return db.Authors
                .Include(a=>a.Authorships)
                .ThenInclude(b=>b.Book)
                .Where(a=>a.AuthorId==id)
                .FirstOrDefault();
        }

        public IEnumerable<Author> GetAll()
        {
            return db.Authors.Include(a => a.Authorships)
                             .ThenInclude(b => b.Book);
        }

        public void Update(Author author)
        {
            var enrty = db.Entry(author);
            db.Update(author);
        }
        public IEnumerable<Author> GetAuthorsByBookId(Guid bookId)
        {
            IEnumerable<Author> authorOfBooks = db.Authors.Include(a => a.Authorships)
                                                          .Where(a => a.Authorships
                                                          .Any(b => b.BookId == bookId));
            return authorOfBooks;

        }
    }

}
