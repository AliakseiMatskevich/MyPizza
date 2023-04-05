using AutoMapper;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.MappingProfile
{
    public class WeightTypeProfiles : Profile
    {
        public WeightTypeProfiles()
        {
            CreateMap<WeightType, WeightTypeViewModel>();
        }       
    }
}
