using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.Messages;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using Buriti_Store.Orders.Application.Events;
using Buriti_Store.Orders.Domain;
using Buriti_Store.Orders.Domain.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Buriti_Store.Orders.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository,
                                   IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByCustomerId(message.ClientId);
            var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.ValueUnity);

            if (order == null)
            {
                order = Order.OrderFactory.NewOrderDraft(message.ClientId);
                order.AddItem(orderItem);

                _orderRepository.Add(order);

                order.AddEvent(new OrderDraftStartedEvent(message.ClientId, message.ProductId));
            }
            else
            {
                var orderItemExists = order.ExistingOrderItem(orderItem);
                order.AddItem(orderItem);

                if (orderItemExists)
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }

                order.AddEvent(new OrderUpdateEvent(order.ClientId, order.Id, order.TotalValue));
            }

            order.AddEvent(new OrderItemAddedEvent(order.ClientId, order.Id,message.ProductId, message.Name, message.ValueUnity, message.Quantity));

            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach(var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
