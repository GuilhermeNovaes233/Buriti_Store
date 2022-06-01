using Buriti_Store.Core.Messages;
using System;

namespace Buriti_Store.Orders.Application.Events
{
    public class VoucherAppliedOrderEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid VoucherId { get; private set; }

        public VoucherAppliedOrderEvent(Guid clientId, Guid orderId, Guid voucherId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            VoucherId = voucherId;
        }
    }
}