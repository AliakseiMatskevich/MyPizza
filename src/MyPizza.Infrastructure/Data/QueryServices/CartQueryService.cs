using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPizza.ApplicationCore.Interfaces;

namespace MyPizza.Infrastructure.Data.QueryServices
{
    public sealed class CartQueryService : ICartQueryService
    {
        private readonly PizzaContext _dbContext;

        public CartQueryService(PizzaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountCartProductsAsync(Guid? userId)
        {
            if (userId == null)
            {
                return 0;
            }
            var total = await _dbContext.Carts
                .Where(x => x.UserId == userId)
                .SelectMany(y => y.Products)
                .SumAsync(i => i.Quantity);

            return total;
        }
    }
}
