using System;

namespace Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderStockRejectedEvent : IntegrationEvent
    {
        public OrderStockRejectedEvent(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }

        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
    }
}