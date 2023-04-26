using MyPizza.Web.Models;

namespace MyPizza.Web.Interfaces
{
    public interface IOrderViewModelService
    {
        Task<IList<OrderViewModel>> GetUserOrdersAsync(Guid userId);
        Task<OrderViewModel> GetLastUserOrderAsync(Guid userId);
        Task<Guid> CreateOrderAsync(OrderViewModel model);
        Task SendOrderConfirmationEmailAsync(OrderViewModel model);
        Task SetOrderAsPaidAsync(Guid orderId);
    }
}
