namespace MyPizza.Web.Models
{
    public class ProductTypeIndexViewModel
    {
        public IList<CategoryViewModel>? Categories { get; set; }
        public IList<ProductTypeViewModel>? ProductTypeItems { get; set; }
    }
}
