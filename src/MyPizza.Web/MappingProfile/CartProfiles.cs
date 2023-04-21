using AutoMapper;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.MappingProfile
{
    public class CartProfiles : Profile
    {
        public CartProfiles()
        {
            CreateMap<Cart, CartViewModel>();
            CreateMap<Cart, OrderViewModel>();
        }        
    }
}
