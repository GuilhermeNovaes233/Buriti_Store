using System;
using System.Collections.Generic;
using System.Text;

namespace Buriti_Store.Orders.Application.Queries.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime DateRegister { get; set; }
        public int OrderStatus { get; set; }
    }
}
