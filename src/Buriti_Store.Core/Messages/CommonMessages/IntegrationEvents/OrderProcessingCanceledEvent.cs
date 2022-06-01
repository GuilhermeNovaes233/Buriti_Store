using Buriti_Store.Core.DomainObjects.DTO;
using System;

namespace Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderProcessingCanceledEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClienteId { get; private set; }
        public ListProductsOrder ProductsOrder { get; private set; }

        public OrderProcessingCanceledEvent(Guid orderId, Guid clienteId, ListProductsOrder listProducts)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClienteId = clienteId;
            ProductsOrder = listProducts;
        }
    }
}
