using Buriti_Store.Core.Messages.CommonMessages.DomainEvents;
using System;

namespace Buriti_store.Catalog.Domain.Events
{
    public class ProductOutOfStockEvent : DomainEvent
    {
        public ProductOutOfStockEvent(Guid aggregateId, int quantityInStock) : base(aggregateId)
        {
            QuantityInStock = quantityInStock;
        }

        public int QuantityInStock { get; private set; }
    }
}