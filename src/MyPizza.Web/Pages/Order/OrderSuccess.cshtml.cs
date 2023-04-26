using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Web.Interfaces;

namespace MyPizza.Web.Pages.Order
{
    public class OrderSuccessModel : PageModel
    {
        private readonly IOrderViewModelService _orderViewModelService;
        public OrderSuccessModel(IOrderViewModelService orderViewModelService)
        {
            _orderViewModelService = orderViewModelService;
        }
        public async Task OnGet(Guid orderId)
        {
            await _orderViewModelService.SetOrderAsPaidAsync(orderId);
        }
    }
}
