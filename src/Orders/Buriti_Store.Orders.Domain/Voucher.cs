using Buriti_Store.Core.DomainObjects;
using Buriti_Store.Orders.Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Buriti_Store.Orders.Domain
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? ValueDiscount { get; private set; }
        public int Quantity { get; private set; }
        public TypeVoucher TypeVoucher { get; private set; }
        public DateTime DateRegister { get; private set; }
        public DateTime? DateUsage { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        // EF Rel.
        public ICollection<Order> Orders { get; set; }

        internal ValidationResult ValidateIfApplicable()
        {
            return new VoucherAplicavelValidation().Validate(this);
        }
    }

    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {
        public VoucherAplicavelValidation()
        {
            RuleFor(c => c.ExpirationDate)
                .Must(ExpirationDateSuperiorCurrent)
                .WithMessage("Este voucher está expirado.");

            RuleFor(c => c.Active)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

            RuleFor(c => c.Used)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Este voucher não está mais disponível");
        }

        protected static bool ExpirationDateSuperiorCurrent(DateTime dataValidade)
        {
            return dataValidade >= DateTime.Now;
        }
    }
}