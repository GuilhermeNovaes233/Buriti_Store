using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.Messages;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using Buriti_Store.Orders.Application.Events;
using Buriti_Store.Orders.Domain;
using Buriti_Store.Orders.Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Buriti_Store.Orders.Application.Commands
{
    public class OrderCommandHandler : 
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<UpdateOrderItemCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>,
        IRequestHandler<ApplyVoucherOrderCommand, bool>
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

                order.AddEvent(new OrderUpdateEvent(order.ClientId, order.Id, order.TotalValue, message.ProductId, message.Quantity));
            }

            order.AddEvent(new OrderItemAddedEvent(order.ClientId, order.Id,message.ProductId, message.Name, message.ValueUnity, message.Quantity));

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByCustomerId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

            if (!order.ExistingOrderItem(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Item do pedido não encontrado!"));
                return false;
            }

            order.UpdateUnits(orderItem, message.Quantity);
            order.AddEvent(new OrderProductUpdatedEvent(message.ClientId, order.Id, message.ProductId, message.Quantity));

            _orderRepository.UpdateItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByCustomerId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Pedido não encontrado!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

            if (orderItem != null && !order.ExistingOrderItem(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Pedido", "Item do pedido não encontrado!"));
                return false;
            }

            order.RemoveItem(orderItem);
            order.AddEvent(new ProductOrderRemovedEvent(message.ClientId, order.Id, message.ProductId));

            _orderRepository.RemoveItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyVoucherOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByCustomerId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "order não encontrado!"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCode(message.CodeVoucher);

            if (voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Voucher não encontrado!"));
                return false;
            }

            var voucherApplicationValidation = order.ApplyVoucher(voucher);
            if (!voucherApplicationValidation.IsValid)
            {
                foreach (var error in voucherApplicationValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            order.AddEvent(new VoucherAppliedOrderEvent(message.ClientId, order.Id, voucher.Id));

            _orderRepository.Update(order);

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