using BookshopBLL.DTO;
using System;
using System.Collections.Generic;

namespace BookshopBLL.Interfaces
{
    public interface IAuthorService
    {
        void Create(AuthorDTO authorDto);
        IEnumerable<BookDTO> GetBooks(Guid authorId);
        IEnumerable<AuthorDTO> GetAll();
        AuthorDTO Get(Guid id);
        void Update(AuthorDTO author);
        AuthorDTO Delete(Guid id);
        void Dispose();
    }
}
