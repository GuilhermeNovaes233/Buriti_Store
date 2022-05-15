using Buriti_Store.Core.Messages;
using System;

namespace Buriti_Store.Orders.Application.Events
{
    public class OrderDraftStartedEvent : Event
    {
        public OrderDraftStartedEvent(Guid clienteId, Guid pedidoId)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
        }

        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }
    }
}