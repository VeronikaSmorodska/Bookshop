using Bookshop.Dapper.Entities;
using Bookshop.Dapper.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bookshop.Dapper.Repositories
{
    class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        public IEnumerable<Author> GetAll()
        {
            IEnumerable<Author> authors = new List<Author>();

            authors = Connection.Query<Author, Book, Author>(
                "SELECT * FROM Authors LEFT JOIN Authorships ON Authorships.AuthorId = Authors.AuthorId LEFT JOIN Books ON Books.BookId=Authorships.BookId",
                (author, book) =>
                {
                    if (book is null)
                    {
                        book = new Book();
                    }
                    if (author.Authorships is null)
                    {
                        author.Authorships = new List<Authorship>();
                    }
                    author.Authorships.Add(new Authorship() { Book = book });
                    return author;
                },
                transaction: Transaction,
                splitOn: "AuthorId,BookId");
            return authors;
        }

        public Author Get(Guid id)
        {
            Author authorById = null;
            authorById = Connection.Query<Author, Book, Author>(
                "SELECT * FROM Authors LEFT JOIN Authorships ON Authors.AuthorId = Authorships.AuthorId LEFT JOIN Books ON Books.BookId = Authorships.BookId  WHERE Authors.AuthorId = @id",
                (author, book) =>
                {
                    if (book is null)
                    {
                        book = new Book();
                    }
                    if (author.Authorships is null)
                    {
                        author.Authorships = new List<Authorship>();
                    }
                    author.Authorships.Add(new Authorship() { Book = book });


                    return author;
                }, new { id },
                transaction: Transaction,
                splitOn: "AuthorId, BookId").FirstOrDefault();
            return authorById;
        }

        public Author Create(Author author)
        {
            var bookIds = new List<Guid>();
            foreach (var authorship in author.Authorships ?? new List<Authorship>())
            {
                bookIds.Add(authorship.BookId);
                if (Connection.Query<Book>("SELECT * FROM Books WHERE BookId = @bookId", new { bookId = authorship.BookId }, transaction: Transaction).Any())
                {
                    continue;
                }
                var innerQuery = "INSERT INTO Books (BookId, Title, Price, Type) Values(@BookId, @Title, @Price, @Type);";
                Connection.Execute(innerQuery, authorship.Book, transaction: Transaction);
            }
            var sqlQuery = "INSERT INTO Authors (AuthorId, Name) VALUES(@AuthorId, @Name);";
            Connection.Execute(sqlQuery, new { author.AuthorId, author.Name }, transaction: Transaction);

            foreach (var authorship in author.Authorships)
            {

                Connection.Execute($"INSERT INTO Authorships (AuthorshipId, AuthorId, BookId) VALUES(@AuthorshipId, @AuthorId, @BookId)",
                                   new { authorship.AuthorshipId, author.AuthorId, authorship.BookId },
                                   transaction: Transaction);
            }
            return author;
        }
        public void Update(Author author)
        {
            var bookIds = new List<Guid>();
            foreach (var authorship in author.Authorships ?? new List<Authorship>())
            {
                bookIds.Add(authorship.BookId);

                if (Connection.Query<Book>("SELECT * FROM Books WHERE BookId = @bookId", new { bookId = authorship.BookId }, transaction: Transaction).Any())
                {
                    continue;
                }

                var innerQuery = "UPDATE Books SET Title=@Title, BookId=@BookId, @Price=Price,Type=@Type;";
                Connection.Execute(innerQuery, new { authorship.Book, authorship.Book.Title, authorship.Book.BookId, authorship.Book.Price, authorship.Book.Type }, transaction: Transaction);
            }
            var sqlQuery = "UPDATE Authors SET Name = @Name WHERE AuthorId = @AuthorId";
            Connection.Execute(sqlQuery, author, transaction: Transaction);

            foreach (var authorship in author.Authorships)
            {
                var existingAuthorship = Connection.Query<Author>("SELECT * FROM Authorships WHERE AuthorId = @AuthorId AND BookId = @BookId ", new { author.AuthorId, authorship.BookId }, transaction: Transaction)
                     .FirstOrDefault();
                if (existingAuthorship == null)
                {
                    Connection.Execute($"INSERT INTO Authorships (AuthorshipId, AuthorId, BookId) VALUES(@AuthorshipId, @AuthorId, @BookId)",
                                   new { authorship.AuthorshipId, author.AuthorId, authorship.BookId },
                                   transaction: Transaction);
                }
            }
        }
        public void Delete(Guid id)
        {
            var author = Connection.Query<Author>("SELECT * FROM Authors WHERE AuthorId = @id ", new { id }, transaction: Transaction)
                     .FirstOrDefault();
            if (author != null)
            {
                var sqlQuery = "DELETE FROM Authors WHERE Authors.AuthorId = @id";
                Connection.Execute(sqlQuery, new { id }, transaction: Transaction);
            }
        }
        public IEnumerable<Author> GetAuthorsByBookId(Guid bookId)
        {
            IEnumerable<Author> authorOfBooks = Connection.Query<Author>(
                "SELECT * FROM Authorships INNER JOIN Authors ON Authorships.AuthorId=Authors.AuthorId WHERE Authorships.BookId=@bookId;",
                 new { bookId },
                transaction: Transaction);
            return authorOfBooks;
        }
    }
}