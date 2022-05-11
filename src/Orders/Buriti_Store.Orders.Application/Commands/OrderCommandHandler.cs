using Buriti_Store.Core.Messages;
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

        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
            }

            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach(var error in message.ValidationResult.Errors)
            {
                //TODO - Lançar um evento de erro
            }

            return false;
        }
    }
}
