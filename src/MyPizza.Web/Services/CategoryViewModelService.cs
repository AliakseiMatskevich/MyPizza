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
        private readonly ILogger<CategoryViewModelService> _logger;

        public CategoryViewModelService(IUoWRepository repository,
            IMapper mapper,
            ILogger<CategoryViewModelService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IList<CategoryViewModel>> GetCategoriesAsync()
        {
            _logger.LogInformation($"Сategories list has been received");
            var categories = await _repository.Categories.GetListAsync();
            var mapCategories = _mapper.Map<List<CategoryViewModel>>(categories);
            return mapCategories;
        }
    }
}
