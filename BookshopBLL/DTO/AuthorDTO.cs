using System;
using System.Collections.Generic;
using System.Text;

namespace BookshopBLL.DTO
{
    public class AuthorDTO
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}
