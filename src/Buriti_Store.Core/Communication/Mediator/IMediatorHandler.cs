using Buriti_Store.Core.Messages;
using Buriti_Store.Core.Messages.CommonMessages.DomainEvents;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Buriti_Store.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notificacao) where T : DomainNotification;
        Task PublishDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}