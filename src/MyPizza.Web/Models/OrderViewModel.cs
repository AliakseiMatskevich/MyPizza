using Microsoft.AspNetCore.Mvc;
using MyPizza.ApplicationCore.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyPizza.Web.Models
{
    public sealed class OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [EmailAddress, Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Street is required"), StringLength(20, ErrorMessage = "Must be between 4 and 20 characters long", MinimumLength = 4)]
        public string? Street { get; set; }
        [BuildingNumber, Required(ErrorMessage = "BuildingNumber is required")]
        public string? BuildingNumber { get; set; }
        [AppartmentNumber, Required(ErrorMessage = "Appartment number is required")]
        public string? AppartmentNumber { get; set; }
        [PhoneNumber, Required(ErrorMessage = "Phone number is required")]
        public string? PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public bool Paid { get; set; }
        public List<OrderProductViewModel> Products { get; set; } = new List<OrderProductViewModel>();

        public OrderViewModel()
        {
            Date = DateTime.UtcNow;
        }
        public decimal Sum()
        {
            return Math.Round(Products.Sum(x => x.Price * x.Quantity), 2);
        }
    }
}
