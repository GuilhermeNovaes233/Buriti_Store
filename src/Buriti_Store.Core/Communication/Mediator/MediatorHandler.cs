using Buriti_Store.Core.Data.EventSourcing;
using Buriti_Store.Core.Messages;
using Buriti_Store.Core.Messages.CommonMessages.DomainEvents;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace Buriti_Store.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediatr;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public MediatorHandler(IMediator mediatr,
                               IEventSourcingRepository eventSourcingRepository)
        {
            _mediatr = mediatr;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediatr.Publish(evento);
            await _eventSourcingRepository.SaveEvent(evento);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediatr.Send(command);
        }


        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediatr.Publish(notification);
        }

        public async Task PublishDomainEvent<T>(T notificacao) where T : DomainEvent
        {
            await _mediatr.Publish(notificacao);
        }
    }
}