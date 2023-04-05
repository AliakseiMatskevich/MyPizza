namespace MyPizza.Web.Models
{
    public sealed class CartViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartProductViewModel> Products { get; set; } = new List<CartProductViewModel>();

        public decimal Sum()
        {
            return Math.Round(Products.Sum(x => x.Price * x.Quantity), 2);
        }
    }
}
