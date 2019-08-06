using AutoMapper;
using BookshopBLL.DTO;
using BookshopBLL.Interfaces;
using System;
using System.Collections.Generic;
using BookshopPersistenceLayer.Interfaces;
using BookshopPersistenceLayer.Entities;

namespace BookshopBLL.Services
{
    public class BookService:IBookService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper mapper;
        public BookService(IUnitOfWork uow, IMapper _mapper)
        {
            Database = uow;
            mapper = _mapper;
        }
        public IEnumerable<BookDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Book>, IEnumerable<BookDTO>>(Database.Books.GetAll());
        }
        public IEnumerable<AuthorDTO> GetAuthors(Guid authorId)
        {
            return mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDTO>>(Database.Authors.GetAuthorsByBookId(authorId));
        }
        public BookDTO Get(Guid id)
        {
            return mapper.Map<Book, BookDTO>(Database.Books.Get(id));
        }
        public void Create(BookDTO bookDTO)
        {
            Database.Books.Create(mapper.Map<Book>(bookDTO));
            Database.Save();
        }
        public BookDTO  Delete(Guid id)
        {
            BookDTO bookDto= mapper.Map<Book, BookDTO>(Database.Books.Delete(id));
            Database.Save();
            return bookDto;
        }
        public void Update(BookDTO bookDTO)
        {
            Database.Books.Update(mapper.Map<Book>(bookDTO));
            Database.Save();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
