using Stripe.Checkout;

namespace MyPizza.Web.Interfaces
{
    public interface IStripeService
    {
        Task<Session> PaymentCreateAsync(decimal amount, Guid orderId);
    }
}
