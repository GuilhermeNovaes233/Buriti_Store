using Buriti_Store.Core.Messages;
using System;

namespace Buriti_Store.Orders.Application.Events
{
    public class OrderItemAddedEvent : Event
    {
        public OrderItemAddedEvent(Guid clientId, Guid orderId, Guid productId, string productName, decimal valueUnitary, int quantity)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            ValueUnitary = valueUnitary;
            Quantity = quantity;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; set; }
        public decimal ValueUnitary { get; private set; }
        public int Quantity { get; private set; }

    }
}
