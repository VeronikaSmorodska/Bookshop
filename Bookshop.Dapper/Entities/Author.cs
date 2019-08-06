using System;
using System.Collections.Generic;

namespace Bookshop.Dapper.Entities
{
    public class Author 
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<Authorship> Authorships { get; set; }
        public Author()
        {
            AuthorId = Guid.NewGuid();
        }
    }
}
