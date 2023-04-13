using MyPizza.ApplicationCore.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Entities
{
    public class Product : BaseEntity
    {
        public Guid ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }
        public Guid? WeightTypeId { get; set; }
        public WeightType? WeigthType { get; set; }
        public int Weight { get; set; }
        public decimal Price { get; set;}
    }
}
