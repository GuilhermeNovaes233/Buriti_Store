using Buriti_store.Catalog.Domain.Events;
using Buriti_store.Catalog.Domain.Interfaces;
using Buriti_Store.Core.Bus;
using System;
using System.Threading.Tasks;

namespace Buriti_store.Catalog.Domain
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatr;

        public StockService(
            IProductRepository productRepository, 
            IMediatorHandler mediatr)
        {
            _productRepository = productRepository;
            _mediatr = mediatr;
        }

        public async Task<bool> DebitStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            
            if (product == null) return false;

            if (!product.HaveStock(quantity)) return false;

            product.DebitStock(quantity);

            if(product.QuantityStock <= 10)
            {
                await _mediatr.PublishEvent(new ProductOutOfStockEvent(product.Id, product.QuantityStock));
            }

            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishStock(quantity);

            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}