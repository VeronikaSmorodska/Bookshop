using BookshopBLL.DTO;
using System;
using System.Collections.Generic;

namespace BookshopBLL.Interfaces
{
    public interface IBookService
    {
        void Create(BookDTO bookDto);
        IEnumerable<AuthorDTO> GetAuthors(Guid authorId);
        IEnumerable<BookDTO> GetAll();
        BookDTO Get(Guid id);
        IEnumerable<BookDTO> GetByTitle(string title);
        void Update(BookDTO bookDto);
        BookDTO Delete(Guid id);
        void Dispose();
    }
}
