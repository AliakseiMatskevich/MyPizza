using MyPizza.ApplicationCore.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Entities
{
    public sealed class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();

        public Cart()
        {            
        }

        public Cart(Guid userId)
        {
            UserId = userId;
        }
        public void AddProduct(Guid productId)
        {
            var existingItem = Products.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem is null)
            {
                Products.Add(new CartProduct(productId));
                return;
            }
           
            existingItem.EncreaseQuantity();
        }

        public void DeleteProduct(Guid productId)
        {
            var existingItem = Products.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem is null)
            {
                return;
            }

            existingItem.DecreaseQuantity();

            if (existingItem.Quantity == 0)
            {
                Products.Remove(existingItem);
            }
        }
    }
}
