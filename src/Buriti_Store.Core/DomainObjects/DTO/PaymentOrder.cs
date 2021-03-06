using System;

namespace Buriti_Store.Core.DomainObjects.DTO
{
    public class PaymentOrder
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal Total { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }
    }
}