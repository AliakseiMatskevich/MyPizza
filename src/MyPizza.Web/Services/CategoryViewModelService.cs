using AutoMapper;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Services
{
    public class CategoryViewModelService : ICategoryViewModelService
    {
        private readonly IUoWRepository _repository;
        private readonly IMapper _mapper;
        public CategoryViewModelService(IUoWRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await _repository.Categories.GetListAsync();
            var mapCategories = _mapper.Map<List<CategoryViewModel>>(categories);
            return mapCategories;
        }
    }
}
