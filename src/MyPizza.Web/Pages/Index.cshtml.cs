using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Entities;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUoWServices _services;

        public ProductTypeIndexViewModel ProductTypeModel { get; set; } = new ProductTypeIndexViewModel();

        public IndexModel(IUoWServices services)
        {
            _services = services;
        }

        public async Task OnGet(Guid? categoryId = null)
        {
            ProductTypeModel = await _services.ProductType.GetProductTypesAsync(categoryId);
        }
    }
}
