using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Data;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using System.Data;
using System.Security.Claims;

namespace MyPizza.Web.Pages.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUoWRepository _repository;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        public IndexModel(IUoWRepository repository, 
                          IUserService userService,
                          ICartService cartService)
        {
            _repository = repository;
            _userService = userService;
            _cartService = cartService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(Guid productId)
        {
            var product = await _repository.Products.GetByIdAsync(productId);
            if (product == null)
            {
                return RedirectToPage("/ProductType/Index");
            }
           
            var userId = _userService.GetUserId(Request.HttpContext.User);

            var cart = await _cartService.AddProductToCart(userId, productId);           

            return RedirectToPage();
        }
    }
}
