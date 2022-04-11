using System.Threading.Tasks;

namespace Buriti_Store.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}