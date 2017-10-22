namespace Webshop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
   
    using React.AspNet;
    using Webshop.Models;
    using Webshop.Utils.Login;
    
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
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<WebshopContext>().AddDefaultTokenProviders();
            services.AddReact();
            services.AddDbContext<WebshopContext>(o => o.UseNpgsql("User ID=postgres;Password=mydatabase;Host=localhost;Port=5432;Database=WebshopDB;Pooling=true;"));
            services.AddMvc();

        

            services.Configure<IdentityOptions>(options =>
            {
             // Password settings
                 options.Password.RequireDigit = true;
                 options.Password.RequiredLength = 8;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = true;
                 options.Password.RequireLowercase = false;
                 options.Password.RequiredUniqueChars = 6;

              // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

              // User settings
                options.User.RequireUniqueEmail = true;
            });

                services.ConfigureApplicationCookie(options =>
                {
                 // Cookie settings
                 options.Cookie.HttpOnly = true;
                 options.Cookie.Expiration = TimeSpan.FromDays(150);
                 options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                 options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                 options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                 options.SlidingExpiration = true;
                });
           }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            System.Console.WriteLine(env.IsDevelopment());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error404");
            }

            // Initialise ReactJS.NET. Must be before static files.
//             app.UseReact(config =>
//             {
//   // If you want to use server-side rendering of React components,
//   // add all the necessary JavaScript files here. This includes
//   // your components as well as all of their dependencies.
//   // See http://reactjs.net/ for more information. Example:
//   //config
//   //  .AddScript("~/Scripts/First.jsx")
//   //  .AddScript("~/Scripts/Second.jsx");
//                });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
