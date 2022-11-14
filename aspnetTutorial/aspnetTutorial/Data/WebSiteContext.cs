using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Data
{
    public class WebSiteContext : IdentityDbContext<User>
    {
        public WebSiteContext()
        {
        }

        public WebSiteContext(DbContextOptions<WebSiteContext> options)
            : base(options)
        {
            
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        

        public DbSet<Book> Book { get; set; }

        


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=Nikkei; Integrated Security=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}
        //Como ya defini la conexion en el startup entonces no debo hacerlo otra vez aqui
    }

}
