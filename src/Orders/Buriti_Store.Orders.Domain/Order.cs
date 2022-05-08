using Buriti_Store.Core.DomainObjects;
using Buriti_Store.Orders.Domain.Enums;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buriti_Store.Orders.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime DateRegister { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        // EF Rel.
        public Voucher Voucher { get; private set; }

        public Order(Guid clientId, bool voucherUsed, decimal discount, decimal totalValue)
        {
            ClientId = clientId;
            VoucherUsed = voucherUsed;
            Discount = discount;
            TotalValue = totalValue;
            _orderItems = new List<OrderItem>();
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public ValidationResult ApplyVoucher(Voucher voucher)
        {
            var validationResult = voucher.ValidateIfApplicable();
            if (!validationResult.IsValid) return validationResult;

            Voucher = voucher;
            VoucherUsed = true;
            CalculateOrderValue();

            return validationResult;
        }

        public void CalculateOrderValue()
        {
            TotalValue = OrderItems.Sum(p => p.CalculateValue());
            CalculateTotalValueDiscount();
        }

        public void CalculateTotalValueDiscount()
        {
            if (!VoucherUsed) return;

            decimal discount = 0;
            var valor = TotalValue;

            if (Voucher.TypeVoucher == TypeVoucher.Percentage)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (valor * Voucher.Percentage.Value) / 100;
                    valor -= discount;
                }
            }
            else
            {
                if (Voucher.ValueDiscount.HasValue)
                {
                    discount = Voucher.ValueDiscount.Value;
                    valor -= discount;
                }
            }

            TotalValue = valor < 0 ? 0 : valor;
            Discount = discount;
        }

        public bool ExistingOrderItem(OrderItem item)
        {
            return _orderItems.Any(p => p.ProductId == item.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if (ExistingOrderItem(item))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);
                existingItem.AddUnits(item.Quantity);
                item = existingItem;

                _orderItems.Remove(existingItem);
            }

            item.CalculateValue();
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.IsValid()) return;

            var existingItem = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("O item não pertence ao pedido");
            _orderItems.Remove(existingItem);

            CalculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var existingItem = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("O item não pertence ao pedido");

            _orderItems.Remove(existingItem);
            _orderItems.Add(item);

            CalculateOrderValue();
        }

        public void UpdateUnits(OrderItem item, int unidades)
        {
            item.UpdateUnits(unidades);
            UpdateItem(item);
        }

        public void MakeDraft()
        {
            OrderStatus = OrderStatus.Sketch;
        }

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Initiated;
        }

        public void FinalizeOrder()
        {
            OrderStatus = OrderStatus.PaidOut;
        }

        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }

        public static class OrderFactory
        {
            public static Order NewOrderDraft(Guid clienteId)
            {
                var order = new Order
                {
                    ClientId = clienteId,
                };

                order.MakeDraft();
                return order;
            }
        }
    }
}