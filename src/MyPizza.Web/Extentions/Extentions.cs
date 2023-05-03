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
                var item = list.Where(x => x.Id == id).FirstOrDefault()!;
                if (item is not null)
                {
                    item.ActiveLink = "active";
                }
            }
            return id;
        }

        public static List<OrderViewModel> SetTimezoneOffset(this List<OrderViewModel> orders, int timezoneOffset)
        {
            orders.ForEach(order => { order.Date = order.Date.AddMinutes(-timezoneOffset); });
            return orders;
        }

        public static OrderViewModel SetTimezoneOffset(this OrderViewModel order, int timezoneOffset)
        {
            order.Date = order.Date.AddMinutes(-timezoneOffset);
            return order;
        }
    }
}
