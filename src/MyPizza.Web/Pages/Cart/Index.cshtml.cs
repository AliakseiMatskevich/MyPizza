using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Attributes.Filter;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Pages.Cart
{
    [TypeFilter(typeof(AppExceptionFilter))]
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUoWRepository _repository;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private readonly ICartViewModelService _cartViewModelService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IUoWRepository repository,
                          IUserService userService,
                          ICartService cartService,
                          ICartViewModelService cartViewModelService,
                          ILogger<IndexModel> logger)
        {
            _repository = repository;
            _userService = userService;
            _cartService = cartService;
            _cartViewModelService = cartViewModelService;
            _logger = logger;
        }

        public CartViewModel CartModel { get; set; } = new CartViewModel();

        public async Task OnGet()
        {
            _logger.LogInformation($"{Request.HttpContext.User.Identity!.Name ?? "Unautorised user"} visit cart index page");

            var userId = _userService.GetUserId(Request.HttpContext.User);

            CartModel = await _cartViewModelService.GetOrCreateCartForUserAsync<CartViewModel>(userId);
        }

        public async Task<IActionResult> OnPostAdd(Guid productId)
        {
            var product = await _repository.Products.GetByIdAsync(productId);
            if (product == null)
            {
                return RedirectToPage("/ProductType/Index");
            }

            _logger.LogInformation($"{Request.HttpContext.User.Identity!.Name ?? "Unautorised user"} add product {productId} to cart");

            var userId = _userService.GetUserId(Request.HttpContext.User);
            await _cartService.AddProductToCartAsync(userId, productId);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(Guid productId)
        {
            var product = await _repository.Products.GetByIdAsync(productId);
            if (product == null)
            {
                return RedirectToPage("/ProductType/Index");
            }

            _logger.LogInformation($"{Request.HttpContext.User.Identity!.Name ?? "Unautorised user"} delete product {productId} from cart");

            var userId = _userService.GetUserId(Request.HttpContext.User);
            await _cartService.DeleteProductFromCartAsync(userId, productId);

            return RedirectToPage();
        }
    }
}
