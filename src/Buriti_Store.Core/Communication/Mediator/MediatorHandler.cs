using Buriti_Store.Core.Messages;
using MediatR;
using System.Threading.Tasks;

namespace Buriti_Store.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediatr;

        public MediatorHandler(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediatr.Publish(evento);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediatr.Send(command);
        }
    }
}