using MyPizza.ApplicationCore.Attributes.Validation;
using MyPizza.ApplicationCore.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.ApplicationCore.Entities
{
    public sealed class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Street is required"), StringLength(20, ErrorMessage = "Must be between 4 and 20 characters long", MinimumLength = 4)]
        public string? Street { get; set; }
        [BuildingNumber, Required(ErrorMessage = "BuildingNumber is required")]
        public string? BuildingNumber { get; set; }
        [AppartmentNumber]
        public string? AppartmentNumber { get; set; }
        [PhoneNumber, Required(ErrorMessage = "Phone number is required")]
        public string? PhoneNumber { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public bool Paid { get; set; }
        public IList<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    }
}
