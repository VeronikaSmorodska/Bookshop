using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopAPI.Models
{
    public class AuthorViewModel
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<BookViewModel> Books { get; set; }
    }
}
