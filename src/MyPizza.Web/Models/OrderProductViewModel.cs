﻿namespace MyPizza.Web.Models
{
    public sealed class OrderProductViewModel
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }
        public string? Measure { get; set; }
        public int Quantity { get; set; }
    }
}
