using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPizza.ApplicationCore.Attributes.Filter;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Pages.Order
{
    [TypeFilter(typeof(AppExceptionFilter))]
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IOrderViewModelService _orderViewModelService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IUserService userService,
                          IOrderViewModelService orderViewModelService,
                          ILogger<IndexModel> logger)
        {
            _userService = userService;
            _orderViewModelService = orderViewModelService;
            _logger = logger;
        }

        public OrderIndexViewModel OrderIndexModel { get; set; } = new OrderIndexViewModel();

        public async Task OnGet()
        {
            _logger.LogInformation($"{Request.HttpContext.User.Identity!.Name ?? "Unautorised user"} visit order index page");

            var userId = _userService.GetUserId(Request.HttpContext.User);

            OrderIndexModel.Orders = await _orderViewModelService.GetUserOrdersAsync(userId);
        }
    }
}
