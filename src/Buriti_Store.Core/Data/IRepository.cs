using Buriti_Store.Core.DomainObjects;
using System;

namespace Buriti_Store.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get;  }
    }
}