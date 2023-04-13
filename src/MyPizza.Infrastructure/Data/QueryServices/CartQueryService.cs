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

        public async Task<int> CountCartProductsAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return 0;
            }

            var total = await _dbContext.Carts.Where(x => x.UserId == userId).
                        Join(_dbContext.CartProducts, c => c.Id, cp => cp.CartId, (c, cp) => cp.Quantity).SumAsync();

            return total;
        }
    }
}
