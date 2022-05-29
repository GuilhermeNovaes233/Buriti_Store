using System;
using Buriti_Store.Core.DomainObjects.DTO;

namespace Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderStockConfirmedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public ListProductsOrder OrderProducts { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardCvv { get; private set; }

        public OrderStockConfirmedEvent(Guid orderId, Guid clientId, decimal total, ListProductsOrder orderProducts, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            OrderProducts = orderProducts;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }
    }
}