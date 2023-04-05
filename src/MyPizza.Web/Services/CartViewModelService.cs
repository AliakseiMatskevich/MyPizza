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

        public CartViewModelService(ICartQueryService cartQueryService,
                                    IUoWRepository repository,
                                    IMapper mapper,
                                    ICartService cartService)
        {
            this._cartQueryService = cartQueryService;
            _repository = repository;
            _mapper = mapper;
            _cartService = cartService;
        }

        public async Task<int> CountCartProductsAsync(Guid userId)
        {
            var amount = await _cartQueryService.CountCartProductsAsync(userId);
            return amount;
        }

        public async Task<CartViewModel> GetOrCreateCartForUser(Guid userId)
        {
            var cart = await _repository.Carts.FirstOrDefaultAsync(
                predicate: x => x.UserId == userId,
                includes: x => x.Include(p => p.Products)
                                .ThenInclude(x => x.Product)!
                                .ThenInclude(x => x!.ProductType!)
                                .ThenInclude(x => x.Category!));

            if (cart == null)
            {
                cart = await _cartService.CreateCart(userId);
            }

            var cartModel = _mapper.Map<CartViewModel>(cart);

            return cartModel;
        }
    }
}
