using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.Interfaces
{
    public interface ICategoryViewModelService
    {
        Task<IList<CategoryViewModel>> GetCategoriesAsync();
    }
}
