using System;
using System.Collections.Generic;

namespace Buriti_Store.Core.DomainObjects.DTO
{
    public class ListProductsOrder
    {
        public Guid OrderId { get; set; }
        public ICollection<Item> Items { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}