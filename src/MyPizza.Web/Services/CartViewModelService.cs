using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Interfaces;
using MyPizza.Web.Interfaces;
using MyPizza.Web.Models;

namespace MyPizza.Web.Services
{
    public class CartViewModelService : ICartViewModelService
    {
        private readonly ICartQueryService _cartQueryService;
        private readonly IUoWRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly ILogger<CartViewModelService> _logger;

        public CartViewModelService(ICartQueryService cartQueryService,
                                    IUoWRepository repository,
                                    IMapper mapper,
                                    ICartService cartService,
                                    ILogger<CartViewModelService> logger)
        {
            this._cartQueryService = cartQueryService;
            _repository = repository;
            _mapper = mapper;
            _cartService = cartService;
            _logger = logger;
        }

        public async Task<int> CountCartProductsAsync(Guid userId)
        {
            _logger.LogInformation($"User with id {userId} got products amount in cart");
            var amount = await _cartQueryService.CountCartProductsAsync(userId);
            return amount;
        }

        public async Task<TViewModel> GetOrCreateCartForUserAsync<TViewModel>(Guid userId)
        {
            _logger.LogInformation($"User with id {userId} get cart");
            var cart = await _repository.Carts.FirstOrDefaultAsync(
                predicate: x => x.UserId == userId,
                includes: x => x.Include(p => p.Products)
                                .ThenInclude(x => x.Product)!
                                .ThenInclude(x => x!.ProductType!)
                                .ThenInclude(x => x.Category!));

            cart ??= await _cartService.CreateCartAsync(userId);

            var cartModel = _mapper.Map<TViewModel>(cart);

            return cartModel;
        }
    }
}
