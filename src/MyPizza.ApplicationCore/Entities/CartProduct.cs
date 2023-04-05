using MyPizza.ApplicationCore.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Entities
{
    public sealed class CartProduct : BaseEntity
    {        
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }

        public CartProduct() { }

        public CartProduct(Guid productId)
        {
            ProductId = productId;
            Quantity = 1;
        }

        public void EncreaseQuantity()
        {
            Quantity++;
        }

        public void DecreaseQuantity()
        {
            if (Quantity > 0)
            {
                Quantity--;
            }            
        }
    }
}
