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
        private readonly ICategoryViewModelService _categoryViewModelService;
        public ProductTypeViewModelService(IUoWRepository repository,
            IMapper mapper,
            ICategoryViewModelService categoryViewModelService) 
        {
            _repository = repository;
            _mapper = mapper;
            _categoryViewModelService = categoryViewModelService;
        }

        public async Task<ProductTypeIndexViewModel> GetProductTypesAsync(Guid? categoryId)
        {
            var categories = await _categoryViewModelService.GetCategoriesAsync();

            var entities = await _repository.ProductTypes.GetListAsync(
                predicate: categoryId == null ? x => x.CategoryId == categories.FirstOrDefault()!.Id : x => x.CategoryId == categoryId,
                includes: x => x.Include(p => p.Category)!
                                .Include(p => p.Products)!,
                orderBy:  x => x.OrderBy(p => p.Category!.Name).ThenBy(p => p.Name));

            

            var vm = new ProductTypeIndexViewModel()
            {
                ProductTypeItems = _mapper.Map<List<ProductTypeViewModel>>(entities),
                Categories = categories
            };
            return vm;
        }
    }
}
