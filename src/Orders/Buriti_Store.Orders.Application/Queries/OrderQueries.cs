using Buriti_Store.Orders.Application.Queries.Interfaces;
using Buriti_Store.Orders.Application.Queries.ViewModels;
using Buriti_Store.Orders.Domain.Enums;
using Buriti_Store.Orders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buriti_Store.Orders.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CartViewModel> GetCartClient(Guid clientId)
        {
            var order = await _orderRepository.GetOrderDraftByCustomerId(clientId);
            if (order == null) return null;

            var cart = new CartViewModel
            {
                ClientId = order.ClientId,
                TotalValue = order.TotalValue,
                OrderId = order.Id,
                ValueDiscount = order.Discount,
                SubTotal = order.Discount + order.TotalValue
            };

            if (order.VoucherId != null)
            {
                cart.CodeVoucher = order.Voucher.Code;
            }

            foreach (var item in order.OrderItems)
            {
                cart.Items.Add(new CartItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitaryValue = item.UnitaryValue,
                    Amount = item.UnitaryValue * item.Quantity
                });
            }

            return cart;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersClient(Guid clientId)
        {
            var orders = await _orderRepository.GetListByClientId(clientId);

            orders = orders.Where(p => p.OrderStatus == OrderStatus.PaidOut || p.OrderStatus == OrderStatus.Canceled)
                .OrderByDescending(p => p.Code);

            if (!orders.Any()) return null;

            var ordersView = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                ordersView.Add(new OrderViewModel
                {
                    Id = order.Id,
                    TotalValue = order.TotalValue,
                    OrderStatus = (int)order.OrderStatus,
                    Code = order.Code,
                    DateRegister = order.DateRegister
                });
            }

            return ordersView;
        }
    }
}
