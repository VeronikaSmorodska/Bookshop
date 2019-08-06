using System;
using System.Collections.Generic;

namespace Bookshop.Dapper.Entities
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public ICollection<Authorship> Authorships { get; set; }

        public Book()
        {
            BookId = Guid.NewGuid();
        }
    }
}
