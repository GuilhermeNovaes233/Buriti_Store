using Buriti_Store.Core.Messages;
using System.Threading.Tasks;

namespace Buriti_Store.Core.Bus
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediatrHandler _mediatr;

        public MediatrHandler(IMediatrHandler mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task PublishEvent<T>(T evento) where T : Event
        {
            await _mediatr.PublishEvent(evento);
        }
    }
}