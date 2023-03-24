using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;
using System.Security.Claims;

namespace MyPizza.Web.Pages.ProducType
{
    [Authorize(Roles = Infrastructure.Constants.ROLE_BUYER)]
    public class IndexModel : PageModel
    {
        private readonly IProductTypeViewModelService _productTypeService;

        public ProductTypeIndexViewModel ProductTypeModel { get; set; } = new ProductTypeIndexViewModel();

        public IndexModel(IProductTypeViewModelService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        public async Task OnGet(Guid? categoryId = null, Guid? weightTypeId = null)
        {           
            ProductTypeModel = await _productTypeService.GetProductTypesAsync(categoryId, weightTypeId);
        }
    }
}
