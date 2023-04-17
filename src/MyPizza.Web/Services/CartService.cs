using Microsoft.EntityFrameworkCore;
using MyPizza.ApplicationCore.Entities;
using MyPizza.ApplicationCore.Interfaces;
using MyPizza.Infrastructure.Data;
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
        private readonly IUoWRepository _repository;
        
        public CartService(IUoWRepository repository)
        {
            _repository = repository;
        }

        private async Task<Cart> GetCartAsync(Guid userId)
        {
            var cart = await _repository.Carts.FirstOrDefaultAsync(predicate: x => x.UserId.Equals(userId),
                                                                 includes: x => x.Include(p => p.Products));

            if (cart == null)
            {
                cart = await CreateCartAsync(userId);
            }

            return cart;
        }

        public async Task<Cart> AddProductToCartAsync(Guid userId, Guid productId)
        {
            var cart = await GetCartAsync(userId);

            cart.AddProduct(productId);
            await _repository.Carts.UpdateAsync(cart);

            return cart;
        }

        public async Task<Cart> ClearCartAsync(Guid userId)
        {
            var cart = await GetCartAsync(userId);

            cart.ClearCart();
            await _repository.Carts.UpdateAsync(cart);

            return cart;
        }

        public async Task<Cart> CreateCartAsync(Guid userId)
        {
            var cart = new Cart(userId);
            return await _repository.Carts.CreateAsync(cart);
        }

        public async Task<Cart> DeleteProductFromCartAsync(Guid userId, Guid productId)
        {
            var cart = await GetCartAsync(userId);

            cart.DeleteProduct(productId);
            await _repository.Carts.UpdateAsync(cart);

            return cart;
        }
    }
}
