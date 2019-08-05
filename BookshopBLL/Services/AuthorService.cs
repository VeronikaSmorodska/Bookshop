using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BookshopBLL.DTO;
using BookshopBLL.Interfaces;
using BookshopPersistenceLayer.Interfaces;
using BookshopPersistenceLayer.Entities;
//using Bookshop.Dapper.Entities;
//using Bookshop.Dapper.Interfaces;

namespace BookshopBLL.Services
{
    public class AuthorService:IAuthorService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper mapper;
        public AuthorService(IUnitOfWork uow, IMapper _mapper)
        {
            Database = uow;
            mapper = _mapper;
        }
        public IEnumerable<AuthorDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Author>, List<AuthorDTO>>(Database.Authors.GetAll());
        }

        public IEnumerable<BookDTO> GetBooks(Guid authorId)
        {
            return mapper.Map<IEnumerable<Book>, List<BookDTO>>(Database.Books.GetBooksByAuthorId(authorId));

        }
        public AuthorDTO Get(Guid id)
        {
            return mapper.Map<Author, AuthorDTO>(Database.Authors.Get(id));
        }
        public void Create(AuthorDTO authorDTO)
        {
            Database.Authors.Create(mapper.Map<Author>(authorDTO));
            Database.Save();
        }

        public void Delete(Guid id)
        {
            Database.Authors.Delete(id);
            Database.Save();
        }
        public void Update(AuthorDTO authorDTO)
        {
            Database.Authors.Update(mapper.Map<Author>(authorDTO));
            Database.Save();
        }
        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
