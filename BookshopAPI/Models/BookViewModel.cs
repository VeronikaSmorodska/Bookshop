using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopAPI.Models
{
    public class BookViewModel
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public ICollection<AuthorViewModel> Authors { get; set; }
    }
}
