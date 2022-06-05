using System;

namespace Buriti_Store.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        public StoredEvent(Guid id, string type, DateTime dateOccurence, string datas)
        {
            Id = id;
            Type = type;
            DateOccurence = dateOccurence;
            Datas = datas;
        }

        public Guid Id { get; private set; }

        public string Type { get; private set; }

        public DateTime DateOccurence { get; set; }

        public string Datas { get; private set; }
    }
}