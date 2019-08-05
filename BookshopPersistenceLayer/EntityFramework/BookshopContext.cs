using BookshopPersistenceLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BookshopPersistenceLayer.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookshopPersistenceLayer.EntityFramework
{
    public class BookshopContext : DbContext
    {
        private string connectionString;

        public BookshopContext(DbContextOptions<BookshopContext> options) : base(options)
        {
        }

        public BookshopContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Authorship> Authorships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    }
}
