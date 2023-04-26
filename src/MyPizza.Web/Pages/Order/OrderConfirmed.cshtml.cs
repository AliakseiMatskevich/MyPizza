using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Pages.Order
{
    public class OrderConfirmedModel : PageModel
    {
        private readonly IStripeService _stripeService;
        public OrderConfirmedModel(IStripeService stripeService)
        {            
            _stripeService = stripeService;
        }
        public OrderConfirmedViewModel OrderModel { get; set; } = new OrderConfirmedViewModel();
       
        public void OnGet(decimal sum, Guid orderId)
        {
            OrderModel.Sum = sum;
            OrderModel.OrderId = orderId;
        }

        public async Task<IActionResult> OnPostCheckOut(decimal sum, Guid orderId)
        {
            var session = await _stripeService.PaymentCreateAsync(sum, orderId);
            Response.Headers.Add("Location", session.Url);
            return StatusCode(303);
        }
    }
}
