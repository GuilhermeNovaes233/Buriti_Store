using Buriti_Store.Core.DomainObjects;
using System;

namespace Buriti_Store.Payment.Business
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public decimal Value { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }

        // EF. Rel.
        public Transaction Transaction { get; set; }
    }
}