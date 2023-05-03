namespace MyPizza.Web.Models
{
    public class ProductTypeIndexViewModel
    {
        public IList<CategoryViewModel>? Categories { get; set; }
        public IList<WeightTypeViewModel>? WeightTypes { get; set; }
        public IEnumerable<ProductViewModel[]>? ProductTypeItems { get; set; }
    }
}
