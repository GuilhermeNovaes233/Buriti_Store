using MediatR;
using System;

namespace Buriti_Store.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}