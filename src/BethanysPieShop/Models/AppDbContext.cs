using BethanysPieShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public class ApplicationDbContextFactory : IDbContextFactory<AppDbContext>
        {
            public AppDbContext Create(DbContextFactoryOptions options)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\v11.0;Database=BethanysPieShop4;Trusted_Connection=True;MultipleActiveResultSets=true");
                return new AppDbContext(optionsBuilder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);           

        }
         

        

        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
