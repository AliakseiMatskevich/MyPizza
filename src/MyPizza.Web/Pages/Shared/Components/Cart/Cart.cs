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
        private readonly ICartQueryService _cartQueryService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public Cart(ICartQueryService cartQueryService,
                    SignInManager<ApplicationUser> signInManager,
                    IUserService userService)
        {
            _cartQueryService = cartQueryService;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userService.GetUserId(HttpContext.User)!;
            var vm = new CartComponentViewModel 
            { 
                ItemsCount = await _cartQueryService.CountCartProductsAsync(userId) 
            };
            return View(vm);
        }
    }
}
