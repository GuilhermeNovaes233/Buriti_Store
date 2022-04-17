using Buriti_Store.Core.Messages;
using System.Threading.Tasks;

namespace Buriti_Store.Core.Bus
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
    }
}