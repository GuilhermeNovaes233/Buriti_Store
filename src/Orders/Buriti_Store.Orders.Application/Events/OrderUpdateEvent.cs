using Buriti_Store.Core.Messages;
using System;

namespace Buriti_Store.Orders.Application.Events
{
    public class OrderUpdateEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalValue { get; set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public OrderUpdateEvent(Guid clientId, Guid orderId, decimal totalValue, Guid productId, int quantity)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            TotalValue = totalValue;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}