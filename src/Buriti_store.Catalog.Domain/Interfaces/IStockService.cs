using Buriti_Store.Core.DomainObjects.DTO;
using System;
using System.Threading.Tasks;

namespace Buriti_store.Catalog.Domain.Interfaces
{
    public interface IStockService : IDisposable
    {
        Task<bool> DebitStock(Guid productId, int quantity);
        Task<bool> ReplenishStock(Guid productId, int quantity);
        Task<bool> DebitListProductsOrder(ListProductsOrder list);
        Task<bool> ReplenishListOrderProducts(ListProductsOrder list);
    }
}