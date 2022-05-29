using Buriti_Store.Core.DomainObjects;
using System;

namespace Buriti_Store.Payment.Business
{
    public class Transaction : Entity
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

        // EF. Rel.
        public Payment Payment { get; set; }
    }
}