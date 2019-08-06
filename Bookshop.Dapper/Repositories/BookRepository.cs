using Bookshop.Dapper.Entities;
using Bookshop.Dapper.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bookshop.Dapper.Repositories
{
    class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        public IEnumerable<Book> GetAll()
        {
            IEnumerable<Book> books = new List<Book>();
            books = Connection.Query<Book, Author, Book>(
                "SELECT * FROM Books LEFT JOIN Authorships ON Authorships.BookId = Books.BookId LEFT JOIN Authors ON Authors.AuthorId=Authorships.AuthorId",
                (book, author) =>
                {
                    if(author is null)
                    {
                        author = new Author();
                    }
                    if (book.Authorships is null)
                    {
                        book.Authorships = new List<Authorship>();
                    }
                    book.Authorships.Add(new Authorship() { Author = author });
                    return book;
                },
                transaction: Transaction,
                splitOn: "BookId,AuthorId");
            return books;
        }
        public Book Get(Guid id)
        {
            Book bookById = null;
            bookById = Connection.Query<Book, Author, Book>(
                 "SELECT * FROM Books LEFT JOIN Authorships ON Books.BookId = Authorships.BookId LEFT JOIN Authors ON Authors.AuthorId = Authorships.AuthorId  WHERE Books.BookId = @id",
                 (book, author) =>
                 {
                     if (author is null)
                     {
                         author = new Author();
                     }
                     if (book.Authorships is null)
                     {
                         book.Authorships = new List<Authorship>();
                     }
                     book.Authorships.Add(new Authorship() { Author = author });


                     return book;
                 }, new { id },
                 transaction: Transaction,
                 splitOn: "BookId,AuthorId").FirstOrDefault();
            return bookById;
        }
        public Book Create(Book book)
        {
            var authorIds = new List<Guid>();
            foreach (var authorship in book.Authorships ?? new List<Authorship>())
            {
                authorIds.Add(authorship.AuthorId);
                if (Connection.Query<Author>("SELECT * FROM Authors WHERE AuthorId = @authorId", new { authorId = authorship.AuthorId }, transaction: Transaction).Any())
                {
                    continue;
                }
                var innerQuery = "INSERT INTO Authors (AuthorId, Name) Values(@AuthorId, @Name);";
                Connection.Execute(innerQuery, authorship.Author, transaction: Transaction);
            }
            var sqlQuery = "INSERT INTO Books (BookId, Title, Price, Type) VALUES(@BookId, @Title, @Price, @Type);";
            Connection.Execute(sqlQuery, new {book.BookId, book.Title, book.Price, book.Type }, transaction: Transaction);

            foreach (var authorship in book.Authorships)
            {

                Connection.Execute($"INSERT INTO Authorships (AuthorshipId, BookId, AuthorId) VALUES(@AuthorshipId, @BookId, @AuthorId)",
                                   new { authorship.AuthorshipId, book.BookId, authorship.AuthorId, },
                                   transaction: Transaction);
            }
            return book;
        }
        public void Update(Book book)
        {
            var authorIds = new List<Guid>();
            foreach (var authorship in book.Authorships ?? new List<Authorship>())
            {
                authorIds.Add(authorship.AuthorId);

                if (Connection.Query<Author>("SELECT * FROM Authors WHERE AuthorId = @authorId", new { authorId = authorship.AuthorId }, transaction: Transaction).Any())
                {
                    continue;
                }

                var innerQuery = "UPDATE Authors SET Name=@Name, AuthorId=@AuthorId;";
                Connection.Execute(innerQuery, new { authorship.Author, authorship.Author.AuthorId }, transaction: Transaction);
            }
            var sqlQuery = "UPDATE Books SET Title = @Title, Price = @Price, Type = @Type WHERE BookId = @BookId";
            Connection.Execute(sqlQuery, book, transaction: Transaction);

            foreach (var authorship in book.Authorships)
            {
                var existingAuthorship = Connection.Query<Book>("SELECT * FROM Authorships WHERE BookId = @BookId AND AuthorId = @AuthorId ", new { book.BookId, authorship.AuthorId }, transaction: Transaction)
                     .FirstOrDefault();
                if (existingAuthorship == null)
                {
                    Connection.Execute($"INSERT INTO Authorships (AuthorshipId, BookId, AuthorId) VALUES(@AuthorshipId, @BookId, @AuthorId)",
                                   new {authorship.AuthorshipId, book.BookId, authorship.AuthorId },
                                   transaction: Transaction);
                }
            }
        }
        public void Delete(Guid id)
        {
            var book = Connection.Query<Book>("SELECT * FROM Books WHERE Books.BookId = @id", new { id }, transaction: Transaction)
                     .FirstOrDefault();
            if (book != null)
            {
                var sqlQuery = "DELETE FROM Books WHERE BookId = @id";
                Connection.Execute(sqlQuery, new { id }, transaction: Transaction);
            }
        }
        public IEnumerable<Book> GetBooksByAuthorId(Guid authorId)
        {
            IEnumerable<Book>  booksOfAuthor = Connection.Query<Book>(
                "SELECT * FROM Authorships INNER JOIN Books ON Authorships.BookId=Books.BookId WHERE Authorships.AuthorId=@authorId;",
                 new { authorId },
                transaction: Transaction);
            return booksOfAuthor;
        }
    }
}

