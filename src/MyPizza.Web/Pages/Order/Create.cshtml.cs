using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Pages.Order
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ICartViewModelService _cartViewModelService;
        private readonly IOrderViewModelService _orderViewModelService;
        private readonly ICartService _cartService;

        public CreateModel(IUserService userService,
                          ICartViewModelService cartViewModelService,
                          IOrderViewModelService orderViewModelService,
                          ICartService cartService) 
        { 
            _userService = userService;
            _cartViewModelService = cartViewModelService;
            _orderViewModelService = orderViewModelService;
            _cartService = cartService;
        }

        [BindProperty]
        public OrderViewModel OrderModel { get; set; } = new OrderViewModel();

        public async Task OnGet()
        {            
            OrderModel.UserId = _userService.GetUserId(Request.HttpContext.User);

            OrderModel = await _orderViewModelService.GetLastUserOrderAsync(OrderModel.UserId);

            var curentOrder = await _cartViewModelService.GetOrCreateCartForUserAsync<OrderViewModel>(OrderModel.UserId);

            OrderModel.Products = curentOrder.Products;            
        }

        public async Task<IActionResult> OnPost()
        {                        
            if (ModelState.IsValid)
            {
                await _orderViewModelService.CreateOrderAsync(OrderModel);
                await _cartService.ClearCartAsync(OrderModel.UserId);
                return RedirectToPage("/Cart/Index");
            }

            return Page();
        }
    }

}
