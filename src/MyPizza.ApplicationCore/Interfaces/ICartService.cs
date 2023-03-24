using MyPizza.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Interfaces
{
    public interface ICartService
    {
        Task<Cart> AddProductToCart(Guid userId, Guid productId);
    }
}
