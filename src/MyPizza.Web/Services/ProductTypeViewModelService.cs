using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Extentions;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;
using System.Collections.Generic;

namespace MyPizza.Web.Services
{
    public class ProductTypeViewModelService : IProductTypeViewModelService
    {
        private readonly IUoWRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICategoryViewModelService _categoryService;
        private readonly IWeightTypeViewModelService _weightTypeService;
        private readonly ILogger<ProductTypeViewModelService> _logger;

        public ProductTypeViewModelService(IUoWRepository repository,
            IMapper mapper,
            ICategoryViewModelService categoryService,
            IWeightTypeViewModelService weightTypeService,
            ILogger<ProductTypeViewModelService> logger) 
        {
            _repository = repository;
            _mapper = mapper;
            _categoryService = categoryService;
            _weightTypeService = weightTypeService;
            _logger = logger;
        }

        public async Task<ProductTypeIndexViewModel> GetProductTypesAsync(Guid? categoryId, Guid? weightTypeId)
        {
            _logger.LogInformation($"Product types list with category {categoryId} and weight type {weightTypeId} has been recieved");
            var categories = await _categoryService.GetCategoriesAsync();
            categoryId = categories.SetActiveItem(categoryId);

            var weightTypes = await _weightTypeService.GetWeihgtTypesAsync(categoryId);
            weightTypeId = weightTypes.SetActiveItem(weightTypeId);

            var entities = await _repository.ProductTypes.GetListAsync(
                predicate: x => x.CategoryId == categoryId,
                includes: x => x.Include(p => p.Category)!                                
                                .Include(p => p.Products!.Where(x => weightTypeId == Guid.Empty || x.WeightTypeId.Equals(weightTypeId)))!,
                orderBy:  x => x.OrderBy(p => p.Category!.Name).ThenBy(p => p.Name));
            
            var vm = new ProductTypeIndexViewModel()
            {
                ProductTypeItems = _mapper.Map<List<ProductViewModel>>(entities),
                Categories = categories,
                WeightTypes = weightTypes
            };
            return vm;
        }
    }
}
