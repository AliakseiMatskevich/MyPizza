namespace MyPizza.Web.Models
{
    public class OrderIndexViewModel
    {
        public IList<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}
