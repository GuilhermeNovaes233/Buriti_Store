using Buriti_Store.Core.Messages;
using System;

namespace Buriti_Store.Orders.Application.Events
{
    public class OrderFinishedEvent : Event
    {
        public Guid OrderId { get; private set; }

        public OrderFinishedEvent(Guid orderId)
        {
            OrderId = orderId;
            AggregateId = orderId;
        }
    }
}