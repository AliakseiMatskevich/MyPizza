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

        public UoWRepository(
            IEFRepository<Category> categories,
            IEFRepository<Product> products,
            IEFRepository<ProductType> productTypes,
            IEFRepository<WeightType> weightTypes)
        {
            Categories = categories;
            Products = products;
            ProductTypes = productTypes;
            WeightTypes = weightTypes;
        }
    }
}
