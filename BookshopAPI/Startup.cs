
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookshopBLL.Interfaces;
using BookshopBLL.Services;
using AutoMapper;
using BookshopBLL.Automapper;
using BookshopAPI.Automapper;
using BookshopPersistenceLayer.Repository;
using BookshopPersistenceLayer.Interfaces;
using System;
using System.Reflection;
using System.Linq;
using BookshopBLL.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookshopAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(options =>
                  {
                      //options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
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
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
