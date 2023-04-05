using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Pages.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUoWRepository _repository;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly ICartViewModelService _cartViewModelService;

        public IndexModel(IUoWRepository repository,
                          IUserService userService,
                          ICartService cartService,
                          ICartViewModelService cartViewModelService)
        {
            _repository = repository;
            _userService = userService;
            _cartService = cartService;
            _cartViewModelService = cartViewModelService;
        }

        public CartViewModel CartModel { get; set; } = new CartViewModel();

        public async Task OnGet()
        {
            var userId = _userService.GetUserId(Request.HttpContext.User);

            CartModel = await _cartViewModelService.GetOrCreateCartForUser(userId);
        }

        public async Task<IActionResult> OnPostAdd(Guid productId)
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

        public async Task<IActionResult> OnPostDelete(Guid productId)
        {
            var product = await _repository.Products.GetByIdAsync(productId);
            if (product == null)
            {
                return RedirectToPage("/ProductType/Index");
            }

            var userId = _userService.GetUserId(Request.HttpContext.User);

            var cart = await _cartService.DeleteProductFromCart(userId, productId);

            return RedirectToPage();
        }
    }
}
