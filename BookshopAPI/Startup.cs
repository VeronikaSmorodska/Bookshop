
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BookshopBLL.Automapper;
using BookshopAPI.Automapper;
using BookshopBLL.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Http;
using Stripe;
using BookshopAPI.Models;

namespace BookshopAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                       .WithOrigins("http://localhost:4200")
                       .AllowCredentials();
            }));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(options =>
                  {
                      options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/api/Account/AccessDenied");
                  });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Install(Configuration);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BLLMappingProfile());
                mc.AddProfile(new APIMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSignalR( options =>
            {
                options.EnableDetailedErrors = true;
            });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSession();

            app.UseCors("CorsPolicy");
            app.UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
