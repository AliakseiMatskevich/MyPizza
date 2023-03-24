using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Services
{
    public class CartViewModelService : ICartViewModelService
    {
        private readonly IUoWRepository _repository;
        public CartViewModelService(IUoWRepository repository)
        {
            _repository = repository;
        }

        public Task<int> CountCartItems(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> GetOrCreateCartForUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
