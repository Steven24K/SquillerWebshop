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
            services.AddReact();
            //services.AddDbContext<WebshopContext>(o => o.UseNpgsql("User ID=postgres;Password=inf2f;Host=localhost;Port=5432;Database=WebshopDB;Pooling=true;"));
            services.AddDbContext<WebshopContext>(o => o.UseSqlite("Data Source=WebshopContext.db"));
            services.AddMvc();
            //services.AddSession(o => o.Cookie.Name = "Session");
            //Add cookie for login session:
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?tabs=aspnetcore2x
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
            //app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
