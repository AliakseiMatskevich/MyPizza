using MyPizza.Web.Models;

namespace MyPizza.Web.Interfaces
{
    public interface ICartViewModelService
    {
        Task<CartViewModel> GetOrCreateCartForUser(Guid userId);
        Task<int> CountCartItems(Guid userId);
    }
}
