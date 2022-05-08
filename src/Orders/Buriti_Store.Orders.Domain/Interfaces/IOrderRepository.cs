using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buriti_Store.Orders.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetListByClientId(Guid clientId);
        Task<Order> GetOrderDraftByCustomerId(Guid clientId);
        void Add(Order order);
        void Update(Order order);

        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
        void AddItem(OrderItem orderItem);
        void UpdateItem(OrderItem orderItem);
        void RemoveItem(OrderItem orderItem);

        Task<Voucher> GetVoucherByCode(string code);
    }
}