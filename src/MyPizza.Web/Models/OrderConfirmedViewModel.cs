namespace MyPizza.Web.Models
{
    public class OrderConfirmedViewModel
    {
        public decimal Sum { get; set; }
        public Guid OrderId { get; set; }
    }
}
