using MyPizza.Web.Interfaces;

namespace MyPizza.Web.Services
{
    public class UoWServices : IUoWServices
    {
        public IProductTypeViewModelService ProductType { get; set; }

        public UoWServices(IProductTypeViewModelService productTypeViewModelService) 
        {
            ProductType = productTypeViewModelService;
        }
    }
}
