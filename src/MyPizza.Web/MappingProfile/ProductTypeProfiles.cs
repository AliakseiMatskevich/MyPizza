using AutoMapper;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.MappingProfile
{
    public class ProductTypeProfiles : Profile
    {
        public ProductTypeProfiles()
        {
            CreateMap<ProductType, ProductTypeViewModel>()
                .ForMember(dto => dto.Measure, opt => opt.MapFrom(entity => entity.Category!.Measure))                
                .ForMember(dto => dto.Price, opt => opt.MapFrom(entity => entity.Products!.FirstOrDefault()!.Price))                
                .ForMember(dto => dto.Weight, opt => opt.MapFrom(entity => entity.Products!.FirstOrDefault()!.Weight));                   
        }
    }
}
