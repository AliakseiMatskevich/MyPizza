using MyPizza.Web.Models;

namespace MyPizza.Web.Interfaces
{
    public interface IOrderViewModelService
    {
        Task<OrderViewModel> GetLastUserOrderAsync(Guid userId);
        Task<OrderViewModel> CreateOrderAsync(OrderViewModel model);
    }
}
