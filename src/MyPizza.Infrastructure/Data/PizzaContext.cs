using Microsoft.EntityFrameworkCore;
using MyPizza.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.Infrastructure.Data
{
    public class PizzaContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<WeightType> WeightTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }        
    }
}
