using System;

namespace Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents
{
    public class RequestPaymentDeclinedEvent : IntegrationEvent
    {
        public RequestPaymentDeclinedEvent(Guid orderId, Guid clientId, Guid paymentId, Guid transactionId, decimal total)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            Total = total;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid PaymentId { get; private set; }
        public Guid TransactionId { get; private set; }
        public decimal Total { get; private set; }
    }
}
