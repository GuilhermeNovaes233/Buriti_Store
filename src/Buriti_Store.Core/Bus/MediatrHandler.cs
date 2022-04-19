using Buriti_Store.Core.Messages;
using MediatR;
using System.Threading.Tasks;

namespace Buriti_Store.Core.Bus
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediatr;

        public MediatrHandler(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediatr.Publish(evento);
        }
    }
}