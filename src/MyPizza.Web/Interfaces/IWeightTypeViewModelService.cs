using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.Interfaces
{
    public interface IWeightTypeViewModelService
    {
        Task<IList<WeightTypeViewModel>> GetWeihgtTypesAsync(Guid? categoryId);
    }
}
