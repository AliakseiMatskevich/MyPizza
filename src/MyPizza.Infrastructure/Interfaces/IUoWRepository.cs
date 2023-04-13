using MyPizza.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.Infrastructure.Interfaces
{
    public interface IUoWRepository
    {
        IEFRepository<Category> Categories { get; }
        IEFRepository<Product> Products { get; }
        IEFRepository<ProductType> ProductTypes { get; }
        IEFRepository<WeightType> WeightTypes { get; }
        IEFRepository<Cart> Carts { get; }
        IEFRepository<CartProduct> CartProducts { get; }
    }
}
