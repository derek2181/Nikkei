using aspnetTutorial.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetTutorial.Repository;

using aspnetTutorial.Models;
using Microsoft.Extensions.Configuration;
using aspnetTutorial.Services;

namespace aspnetTutorial
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration){
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebSiteContext>(options=>options.UseSqlServer(_configuration.GetConnectionString("NikkeiConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebSiteContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = true;
            });
            //services.ConfigureApplicationCookie(config=> {
            //    config.LoginPath = "/Login/SignIn";
            //});
            services.AddControllersWithViews();


#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
            
            //Para quitar las validaciones del lado del cliente
                //.AddViewOptions(option=> {
                //option.HtmlHelperOptions.ClientValidationEnabled = false;
                //});
#endif

            services.AddScoped<INikkeiRepository, NikkeiRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<SMTPConfigModal>(_configuration.GetSection("SMTPConfig"));
            services.AddAuthentication().AddGoogle(options=> {

                options.ClientId = "777472097040-d5qe3e39ks74mjtm52aoan7lfq0c28ju.apps.googleusercontent.com";
                options.ClientSecret = "Ahu9f8U5nGXi6aACioLZ12NX";

            });
            services.AddAuthentication().AddFacebook(options => {

                options.ClientId = "573461856964499";
                options.ClientSecret = "aeee0693e9ccaaaec604c8535d90ec20";

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "Defaults",
                    pattern: "Nikkei/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
