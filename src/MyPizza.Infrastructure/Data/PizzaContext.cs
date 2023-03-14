using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Infrastructure.Data.Identity;

namespace MyPizza.Infrastructure.Data
{
    public class PizzaContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<WeightType> WeightTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }        
    }
}
