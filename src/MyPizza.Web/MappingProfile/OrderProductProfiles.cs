using AutoMapper;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.MappingProfile
{
    public class OrderProductProfiles : Profile
    {
        public OrderProductProfiles()
        {
            CreateMap<OrderProduct, OrderProductViewModel>();
            CreateMap<OrderProductViewModel, OrderProduct>();
        }
    }
}
