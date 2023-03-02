using MyPizza.ApplicationCore.Entities;
using MyPizza.ApplicationCore.Interfaces;

namespace MyPizza.Infrastructure.Data
{
    public sealed class UoWRepository
    {
        public IRepository<Category> Categories;

        public UoWRepository(IRepository<Category> categories)
        {
            Categories = categories;
        }
    }
}
