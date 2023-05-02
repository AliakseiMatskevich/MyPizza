using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Attributes.Filter;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Web.Interfaces;

namespace MyPizza.Web.Pages.Order
{
    [TypeFilter(typeof(AppExceptionFilter))]
    [Authorize]
    public class OrderSuccessModel : PageModel
    {
        private readonly IOrderViewModelService _orderViewModelService;
        private readonly ILogger<OrderSuccessModel> _logger;

        public OrderSuccessModel(IOrderViewModelService orderViewModelService, ILogger<OrderSuccessModel> logger)
        {
            _orderViewModelService = orderViewModelService;
            _logger = logger;

        }
        public async Task OnGet(Guid orderId)
        {
            _logger.LogInformation($"User {Request.HttpContext.User.Identity!.Name} successfully paid order {orderId}");
            await _orderViewModelService.SetOrderAsPaidAsync(orderId);
        }
    }
}
