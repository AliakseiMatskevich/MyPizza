namespace MyPizza.Web.Models
{
    public sealed class CartViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal Sum()
        {
            return Math.Round(Items.Sum(x => x.Price * x.Quantity), 2);
        }
    }
}
