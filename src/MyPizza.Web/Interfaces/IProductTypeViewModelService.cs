using MyPizza.Web.Models;

namespace MyPizza.Web.Interfaces
{
    public interface IProductTypeViewModelService
    {
        Task<ProductTypeIndexViewModel> GetProductTypesAsync(Guid? categoryId, Guid? weightTypeId);
    }
}
