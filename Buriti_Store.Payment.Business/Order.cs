using System;
using System.Collections.Generic;

namespace Buriti_Store.Payment.Business
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public List<Product> Product { get; set; }
    }
}