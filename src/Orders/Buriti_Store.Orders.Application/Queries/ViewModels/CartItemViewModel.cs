using System;

namespace Buriti_Store.Orders.Application.Queries.ViewModels
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitaryValue { get; set; }
        public decimal Amount { get; set; }
    }
}