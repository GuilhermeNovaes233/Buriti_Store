using Buriti_store.Catalog.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Buriti_store.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductOutOfStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductOutOfStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);

            //Enviar e-mail para aquisição de mais produtos
        }
    }
}