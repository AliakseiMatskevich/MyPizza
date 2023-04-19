using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;
using MyPizza.Web.Models.AbstractViewModel;

namespace MyPizza.Web.Extentions
{
    public static class Extentions
    {
        public static Guid? SetActiveItem<T>(this IList<T> list, Guid? id) where T : NavLinkViewModel
        {
            id = list.Count > 0 ?
                                id ?? list.FirstOrDefault()!.Id :
                                default;
            if (list.Count > 0)
            {
                list.Where(x => x.Id == id).FirstOrDefault()!.ActiveLink = "active";
            }

            return id;
        }
    }
}
