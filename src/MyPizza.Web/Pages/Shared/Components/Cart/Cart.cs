using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Data.Identity;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Pages.Shared.Components.Cart
{
    public class Cart: ViewComponent
    {
        private readonly ICartViewModelService _cartService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public Cart(ICartViewModelService cartService,
                    SignInManager<ApplicationUser> signInManager,
                    IUserService userService)
        {
            _cartService = cartService;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userService.GetUserId(HttpContext.User)!;
            var vm = new CartComponentViewModel 
            { 
                ProductsCount = await _cartService.CountCartProductsAsync(userId) 
            };
            return View(vm);
        }
    }
}
