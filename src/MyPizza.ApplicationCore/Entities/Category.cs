using MyPizza.ApplicationCore.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Entities
{
    public sealed class Category : BaseEntity
    {
        [MaxLength(500)]
        public string? Name { get; set; }
        [MaxLength(5)]
        public string? Measure{ get; set; }
        public List<WeightType>? WeightTypes { get; set; }
        public List<ProductType>? ProductTypes { get; set; }

    }
}
