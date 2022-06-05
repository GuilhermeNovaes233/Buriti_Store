using Buriti_Store.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buriti_Store.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveEvent<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}