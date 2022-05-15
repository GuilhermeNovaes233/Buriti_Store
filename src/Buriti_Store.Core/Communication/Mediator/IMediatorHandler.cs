using Buriti_Store.Core.Messages;
using System.Threading.Tasks;

namespace Buriti_Store.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
    }
}