using AutoMapper;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.MappingProfile
{
    public class OrderProductProfiles : Profile
    {
        public OrderProductProfiles()
        {
            CreateMap<OrderProduct, OrderProductViewModel>()
                .ForMember(dto => dto.Measure, opt => opt.MapFrom(entity => entity.Product!.ProductType!.Category!.Measure))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Product!.ProductType!.Name))
                .ForMember(dto => dto.PictureUrl, opt => opt.MapFrom(entity => entity.Product!.ProductType!.PictureUrl))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(entity => entity.Product!.Weight));
            CreateMap<OrderProductViewModel, OrderProduct>();
        }
    }
}
