using BookshopBLL.Interfaces;
using BookshopBLL.Services;
using BookshopPersistenceLayer.Interfaces;
using BookshopPersistenceLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookshopBLL.Infrastructure
{
    public static class MyInstaller
    {
        public static void Install(this IServiceCollection services, IConfiguration config)
        {
            var connection = config.GetConnectionString("connection");
            services.AddDbContext<BookshopPersistenceLayer.EntityFramework.BookshopContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
