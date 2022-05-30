﻿using System.Linq;
using System.Threading.Tasks;
using Buriti_Store.Core.DomainObjects;
using Buriti_Store.Payments.Data;

namespace Buriti_Store.Core.Communication.Mediator
{
    public static class MediatorExtension
    {
        public static async Task PublishEvent(this IMediatorHandler mediator, PaymentContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}