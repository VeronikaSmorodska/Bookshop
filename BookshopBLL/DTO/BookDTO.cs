﻿using System;
using System.Collections.Generic;

namespace BookshopBLL.DTO
{
    public class BookDTO
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public ICollection<AuthorDTO> Authors { get; set; }
        public BookDTO()
        {
            BookId = Guid.NewGuid();
        }
    }
}
