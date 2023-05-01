using Azure;
using Microsoft.AspNetCore.Mvc;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Web.Interfaces;
using Stripe.Checkout;

namespace MyPizza.Web.Services.Stripe
{
    public class StripeService : IStripeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public StripeService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Session> PaymentCreateAsync(decimal amount, Guid orderId)
        {
            var options = new SessionCreateOptions
            {                
                PaymentMethodTypes = new List<string> 
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = Convert.ToInt32(amount * 100), // Price is in USD cents.
                            Currency = "USD",                            
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Total order sum:",
                                Description = $"Order number: {orderId}"
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",               
                SuccessUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}" +
                             $"://{_httpContextAccessor.HttpContext.Request.Host}/Order/OrderSuccess?orderId={orderId}", 
                CancelUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}" +
                            $"://{_httpContextAccessor.HttpContext.Request.Host}/Order/OrderFailed",  
                Locale = "en"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return session;            
        }
    }
}
