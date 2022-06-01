using Buriti_Store.Core.Messages;
using System;

namespace Buriti_Store.Orders.Application.Events
{
    public class OrderDraftStartedEvent : Event
    {
        public OrderDraftStartedEvent(Guid clientId, Guid orderId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}