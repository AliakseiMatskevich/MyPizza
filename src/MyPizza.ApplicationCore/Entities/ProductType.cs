using MyPizza.ApplicationCore.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Entities
{
    public class ProductType : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Name { get; set; }
        public string? Description  { get; set; }
        IList<Product> Products { get; set;}
    }
}
