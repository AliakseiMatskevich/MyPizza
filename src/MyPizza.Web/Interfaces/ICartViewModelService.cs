using MyPizza.Web.Models;

namespace MyPizza.Web.Interfaces
{
    public interface ICartViewModelService
    {
        Task<TViewModel> GetOrCreateCartForUserAsync<TViewModel>(Guid userId);
        Task<int> CountCartProductsAsync(Guid userId);
    }
}
