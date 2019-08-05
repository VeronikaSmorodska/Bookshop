using AutoMapper;
using BookshopBLL.DTO;
using System.Collections.Generic;
using System;
//using Bookshop.Dapper.Entities;
using BookshopPersistenceLayer.Entities;

namespace BookshopBLL.Automapper
{
    public class BLLMappingProfile : Profile
    {
        public BLLMappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();



            CreateMap<Book, BookDTO>().AfterMap((book, bookDTO, context) =>
            {
                if (book.BookId == null)
                {
                    book.BookId = Guid.NewGuid();
                }
                bookDTO.Authors = new List<AuthorDTO>();
                if (book.Authorships == null)
                {
                    return;
                }
                
                foreach (var authorship in book.Authorships)
                {
                    bookDTO.Authors.Add(new AuthorDTO()
                    {
                        AuthorId = authorship.Author.AuthorId,
                        Name = authorship.Author.Name,
                       
                    });

                }
            });

            CreateMap<BookDTO, Book>().AfterMap((bookDTO, book, context) =>
            {
                if (bookDTO.BookId == null)
                {
                    bookDTO.BookId = Guid.NewGuid();
                }
                book.Authorships = new List<Authorship>();
                foreach (var author in bookDTO.Authors)
                {
                    book.Authorships.Add(new Authorship()
                    {
                        BookId = bookDTO.BookId,
                        AuthorId = author.AuthorId
                    });
                    
                }
            });


            CreateMap<Author, AuthorDTO>().AfterMap((author, authorDTO, context) =>
            {

                if (authorDTO.AuthorId == null)
                {
                    authorDTO.AuthorId = Guid.NewGuid();
                }
                authorDTO.Books = new List<BookDTO>();
                if (author.Authorships == null)
                {
                    return;
                }
                foreach (var authorship in author.Authorships)
                {
                    authorDTO.Books.Add( new BookDTO()
                    {
                        BookId = authorship.Book.BookId,
                        Title = authorship.Book.Title,
                        Price = authorship.Book.Price
                    });
                    
                }

            });

            CreateMap<AuthorDTO, Author>().AfterMap((authorDTO, author, context) =>
            {

                if (author.AuthorId == null)
                {
                    author.AuthorId = Guid.NewGuid();
                }
                author.Authorships = new List<Authorship>();
                foreach (var book in authorDTO.Books)
                {
                    author.Authorships.Add(new Authorship()
                    {
                        AuthorId = authorDTO.AuthorId,
                        BookId = book.BookId
                    });
                    
                }
            });
        }
    }
}
