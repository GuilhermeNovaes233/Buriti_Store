using System;
using System.Collections.Generic;

namespace Buriti_Store.Orders.Application.Queries.ViewModels
{
    public class CartViewModel
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalValue { get; set; }
        public decimal ValueDiscount { get; set; }
        public string CodeVoucher { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public PaymentCartViewModel Payment { get; set; }
    }
}