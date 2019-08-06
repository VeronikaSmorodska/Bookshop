using System;

namespace Bookshop.Dapper.Entities
{
    public class Authorship 
    {
        public Guid AuthorshipId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
        public Authorship()
        {
            AuthorshipId = Guid.NewGuid();
        }
    }
}
