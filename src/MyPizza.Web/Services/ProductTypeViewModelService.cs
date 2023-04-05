using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Services
{
    public class ProductTypeViewModelService : IProductTypeViewModelService
    {
        private readonly IUoWRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICategoryViewModelService _categoryService;
        private readonly IWeightTypeViewModelService _weightTypeService;
        public ProductTypeViewModelService(IUoWRepository repository,
            IMapper mapper,
            ICategoryViewModelService categoryService,
            IWeightTypeViewModelService weightTypeService) 
        {
            _repository = repository;
            _mapper = mapper;
            _categoryService = categoryService;
            _weightTypeService = weightTypeService;
        }

        public async Task<ProductTypeIndexViewModel> GetProductTypesAsync(Guid? categoryId, Guid? weightTypeId)
        {
            var categories = await _categoryService.GetCategoriesAsync();
            categoryId = categoryId ?? categories.FirstOrDefault()!.Id;

            var weightTypes = await _weightTypeService.GetWeihgtTypesAsync(categoryId);
            weightTypeId = weightTypes.Count > 0 ? 
                                weightTypeId ?? weightTypes.FirstOrDefault()!.Id:
                                default;

            var entities = await _repository.ProductTypes.GetListAsync(
                predicate: x => x.CategoryId == categoryId,
                includes: x => x.Include(p => p.Category)!                                
                                .Include(p => p.Products!.Where(x => weightTypeId != Guid.Empty ? x.WeightTypeId.Equals(weightTypeId) : true))!,
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
