using BookshopMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopMVC.Data
{
    public class BookshopConext : DbContext
    {
        public BookshopConext(DbContextOptions<BookshopConext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Authorship> Authopships { get; set; }


    }
}
