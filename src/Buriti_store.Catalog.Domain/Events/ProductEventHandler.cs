using Buriti_store.Catalog.Domain.Interfaces;
using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents;
using Buriti_Store.Orders.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Buriti_store.Catalog.Domain.Events
{
    public class ProductEventHandler : 
        INotificationHandler<ProductOutOfStockEvent>,
        INotificationHandler<OrderInitiatedEvent>,
        INotificationHandler<OrderProcessingCanceledEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProductEventHandler(IProductRepository productRepository,
                                   IStockService stockService,
                                   IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(ProductOutOfStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);

            //Enviar e-mail para aquisição de mais produtos
        }

        public async Task Handle(OrderInitiatedEvent message, CancellationToken cancellationToken)
        {
            var result = await _stockService.DebitListProductsOrder(message.OrdersProducts);

            if (result)
            {
                await _mediatorHandler.PublishEvent(new OrderStockConfirmedEvent(message.OrderId, message.ClientId, message.Total, message.OrdersProducts, message.CardName, message.CardNumber, message.CardExpiration, message.CardCvv));
            }
            else
            {
                await _mediatorHandler.PublishEvent(new OrderStockRejectedEvent(message.OrderId, message.ClientId));
            }
        }

        public async Task Handle(OrderProcessingCanceledEvent message, CancellationToken cancellationToken)
        {
            await _stockService.ReplenishListOrderProducts(message.ProductsOrder);
        }
    }
}