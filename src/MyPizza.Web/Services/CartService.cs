using MyPizza.ApplicationCore.Entities;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.Web.Services
{
    public sealed class CartService : ICartService
    {
        private readonly IEFRepository<Cart> _cartRepository;
        public CartService(IEFRepository<Cart> repositoryCart)
        {
            _cartRepository = repositoryCart;
        }
        public async Task<Cart> AddProductToCart(Guid userId, Guid productId)
        {
            var cart = await _cartRepository.FirstOrDefaultAsync(predicate: x => x.UserId.Equals(userId));

            if (cart == null)
            {
                cart = new Cart(userId);
                cart = await _cartRepository.CreateAsync(cart);
            }

            cart.AddProduct(productId);
            await _cartRepository.UpdateAsync(cart);

            return cart;
        }
    }
}
