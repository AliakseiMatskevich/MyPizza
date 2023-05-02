using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Attributes.Filter;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Pages.Order
{
    [TypeFilter(typeof(AppExceptionFilter))]
    [Authorize]
    public class OrderConfirmedModel : PageModel
    {
        private readonly IStripeService _stripeService;
        private readonly ILogger<OrderConfirmedModel> _logger;
        public OrderConfirmedModel(IStripeService stripeService, ILogger<OrderConfirmedModel> logger)
        {
            _stripeService = stripeService;
            _logger = logger;
        }
        public OrderConfirmedViewModel OrderModel { get; set; } = new OrderConfirmedViewModel();
       
        public void OnGet(decimal sum, Guid orderId)
        {
            _logger.LogInformation($"User {Request.HttpContext.User.Identity!.Name} successfully create order {orderId}");
            OrderModel.Sum = sum;
            OrderModel.OrderId = orderId;
        }

        public async Task<IActionResult> OnPostCheckOut(decimal sum, Guid orderId)
        {
            _logger.LogInformation($"User {Request.HttpContext.User.Identity!.Name} try to pay order {orderId}");
            var session = await _stripeService.PaymentCreateAsync(sum, orderId);
            Response.Headers.Add("Location", session.Url);
            return StatusCode(303);
        }
    }
}
