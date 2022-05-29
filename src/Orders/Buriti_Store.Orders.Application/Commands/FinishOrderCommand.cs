using Buriti_Store.Core.Messages;
using System;

namespace Buriti_Store.Orders.Application.Commands
{
    public class FinishOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }

        public FinishOrderCommand(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }
    }
}