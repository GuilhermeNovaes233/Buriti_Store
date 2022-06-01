using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents;
using Buriti_Store.Orders.Application.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Buriti_Store.Orders.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<OrderDraftStartedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderStockRejectedEvent>,
        INotificationHandler<OrderPaymentAccomplishedEvent>,
        INotificationHandler<RequestPaymentDeclinedEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(OrderDraftStartedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(OrderStockRejectedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelProcessingOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(OrderPaymentAccomplishedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new FinishOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(RequestPaymentDeclinedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelProcessingOrderReturnStockCommand(message.OrderId, message.ClientId));
        }
    }
}