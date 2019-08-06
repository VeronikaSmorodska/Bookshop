using System;
using System.Collections.Generic;

namespace BookshopBLL.DTO
{
    public class AuthorDTO
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<BookDTO> Books { get; set; }
        public AuthorDTO()
        {
            AuthorId = Guid.NewGuid();
        }
    }
}
