using MyPizza.Web.Models;

namespace MyPizza.Web.Interfaces
{
    public interface IOrderViewModelService
    {
        Task<IList<OrderViewModel>> GetUserOrdersAsync(Guid userId);
        Task<OrderViewModel> GetLastUserOrderAsync(Guid userId);
        Task<OrderViewModel> CreateOrderAsync(OrderViewModel model);
        Task SendOrderConfirmationEmailAsync(OrderViewModel model);
    }
}
