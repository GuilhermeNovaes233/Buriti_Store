using Buriti_store.Catalog.Domain.Events;
using Buriti_store.Catalog.Domain.Interfaces;
using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.DomainObjects.DTO;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
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
            if (!await DebitItemStock(productId, quantity)) return false;

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitListProductsOrder(ListProductsOrder list)
        {
            foreach (var item in list.Items)
            {
                if (!await DebitItemStock(item.Id, item.Quantity)) return false;
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            if (!product.HaveStock(quantity))
            {
                await _mediatr.PublishNotification(new DomainNotification("Estoque", $"product - {product.Name} sem estoque"));
                return false;
            }

            product.DebitStock(quantity);

            // TODO: 10 pode ser parametrizavel em arquivo de configuração
            if (product.QuantityStock < 10)
            {
                await _mediatr.PublishDomainEvent(new ProductOutOfStockEvent(product.Id, product.QuantityStock));
            }

            _productRepository.Update(product);
            return true;
        }

        public async Task<bool> ReplenishListOrderProducts(ListProductsOrder list)
        {
            foreach (var item in list.Items)
            {
                await ReplenishItemStock(item.Id, item.Quantity);
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            var sucesso = await ReplenishItemStock(productId, quantity);

            if (!sucesso) return false;

            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ReplenishItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishStock(quantity);

            _productRepository.Update(product);

            return true;
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}