using AutoMapper;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Models;

namespace MyPizza.Web.MappingProfile
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
