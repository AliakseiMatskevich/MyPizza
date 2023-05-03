using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Attributes.Filter;

namespace MyPizza.Web.Pages.Order
{
    [TypeFilter(typeof(AppExceptionFilter))]
    [Authorize]
    public class OrderFailedModel : PageModel
    {
        private readonly ILogger<OrderFailedModel> _logger;
        public OrderFailedModel(ILogger<OrderFailedModel> logger)
        {
            _logger = logger;
        }
        public void OnGet(Guid orderId)
        {
            _logger.LogInformation($"User {Request.HttpContext.User.Identity!.Name} failed order {orderId} payment");
        }
    }
}
