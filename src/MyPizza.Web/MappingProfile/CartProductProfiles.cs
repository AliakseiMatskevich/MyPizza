using AutoMapper;
using Microsoft.CodeAnalysis;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.MappingProfile
{
    public class CartProductProfiles : Profile
    {
        public CartProductProfiles()
        {
            CreateMap<CartProduct, CartProductViewModel>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Product!.ProductType!.Name))
                .ForMember(dto => dto.Description, opt => opt.MapFrom(entity => entity.Product!.ProductType!.Description))
                .ForMember(dto => dto.PictureUrl, opt => opt.MapFrom(entity => entity.Product!.ProductType!.PictureUrl))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(entity => entity.Product!.Price))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(entity => entity.Product!.Weight))
                .ForMember(dto => dto.Measure, opt => opt.MapFrom(entity => entity.Product!.ProductType!.Category!.Measure));

            CreateMap<CartProduct, OrderProductViewModel>()
                .ForMember(dto => dto.Name, opt => opt.MapFrom(entity => entity.Product!.ProductType!.Name))
                .ForMember(dto => dto.PictureUrl, opt => opt.MapFrom(entity => entity.Product!.ProductType!.PictureUrl))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(entity => entity.Product!.Price))
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(entity => entity.Product!.Weight))
                .ForMember(dto => dto.Measure, opt => opt.MapFrom(entity => entity.Product!.ProductType!.Category!.Measure));
        }
    }
}
