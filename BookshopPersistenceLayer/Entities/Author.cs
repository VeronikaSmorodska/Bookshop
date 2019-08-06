using System;
using System.Collections.Generic;

namespace BookshopPersistenceLayer.Entities
{
    public class Author 
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<Authorship> Authorships { get; set; }
    }
}
