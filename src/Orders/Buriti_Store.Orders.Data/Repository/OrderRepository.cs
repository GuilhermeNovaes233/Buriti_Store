using Buriti_Store.Core.Data;
using Buriti_Store.Orders.Domain;
using Buriti_Store.Orders.Domain.Enums;
using Buriti_Store.Orders.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buriti_Store.Orders.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetListByClientId(Guid clienteId)
        {
            return await _context.Orders.AsNoTracking().Where(p => p.ClientId == clienteId).ToListAsync();
        }

        public async Task<Order> GetOrderDraftByCustomerId(Guid clienteId)
        {
            var pedido = await _context.Orders.FirstOrDefaultAsync(p => p.ClientId == clienteId && p.OrderStatus == OrderStatus.Sketch);
            if (pedido == null) return null;

            await _context.Entry(pedido)
                .Collection(i => i.OrderItems).LoadAsync();

            if (pedido.VoucherId != null)
            {
                await _context.Entry(pedido)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return pedido;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }


        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrder(Guid pedidoId, Guid produtoId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(p => p.ProductId == produtoId && p.OrderId == pedidoId);
        }

        public void AddItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
        }

        public void UpdateItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
        }

        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
