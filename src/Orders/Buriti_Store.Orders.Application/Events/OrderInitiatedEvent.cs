using Buriti_Store.Core.DomainObjects.DTO;
using Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents;
using System;

namespace Buriti_Store.Orders.Application.Events
{
    public class OrderInitiatedEvent : IntegrationEvent
    {
        public OrderInitiatedEvent(Guid orderId, Guid clientId, decimal total, ListProductsOrder ordersProducts, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            OrdersProducts = ordersProducts;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public ListProductsOrder OrdersProducts { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardCvv { get; private set; }
    }
}
