using MyPizza.ApplicationCore.Entities;
using MyPizza.Infrastructure.Interfaces;

namespace MyPizza.Infrastructure.Data
{
    public sealed class UoWRepository : IUoWRepository
    {
        public IEFRepository<Category> Categories { get; }
        public IEFRepository<Product> Products { get; }
        public IEFRepository<ProductType> ProductTypes { get; }
        public IEFRepository<WeightType> WeightTypes { get; }
        public IEFRepository<Cart> Carts { get; }
        public IEFRepository<CartProduct> CartProducts { get; }
        public IEFRepository<Order> Orders { get; }
        public IEFRepository<OrderProduct> OrderProducts { get; }

        public UoWRepository(
            IEFRepository<Category> categories,
            IEFRepository<Product> products,
            IEFRepository<ProductType> productTypes,
            IEFRepository<WeightType> weightTypes,
            IEFRepository<Cart> carts,
            IEFRepository<CartProduct> cartProducts,
            IEFRepository<Order> orders,
            IEFRepository<OrderProduct>orderProducts)
        {
            Categories = categories;
            Products = products;
            ProductTypes = productTypes;
            WeightTypes = weightTypes;
            Carts = carts;
            CartProducts = cartProducts;
            Orders = orders;
            OrderProducts = orderProducts;
        }
    }
}
