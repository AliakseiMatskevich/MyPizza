using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Attributes.Filter;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;
using System.Security.Claims;

namespace MyPizza.Web.Pages.ProducType
{
    [TypeFilter(typeof(AppExceptionFilter))]
    public class IndexModel : PageModel
    {
        private readonly IProductTypeViewModelService _productTypeService;
        private readonly ILogger<IndexModel> _logger;

        public ProductTypeIndexViewModel ProductTypeModel { get; set; } = new ProductTypeIndexViewModel();

        public IndexModel(IProductTypeViewModelService productTypeService,
                          ILogger<IndexModel> logger)
        {
            _productTypeService = productTypeService;
            _logger = logger;
        }

        public async Task OnGet(Guid? categoryId = null, Guid? weightTypeId = null)
        {
            //throw new NotImplementedException();
            _logger.LogInformation($"{Request.HttpContext.User.Identity!.Name ?? "Unautorised user"} visited product type page");
            ProductTypeModel = await _productTypeService.GetProductTypesAsync(categoryId, weightTypeId);
        }
    }
}
