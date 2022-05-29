using System;
using Buriti_Store.Core.Messages;
using FluentValidation;

namespace Buriti_Store.Orders.Application.Commands
{
    public class ApplyVoucherOrderCommand : Command
    {
        public Guid ClientId { get; private set; }
        public string CodeVoucher { get; private set; }

        public ApplyVoucherOrderCommand(Guid clientId, string codeVoucher)
        {
            ClientId = clientId;
            CodeVoucher = codeVoucher;
        }

        public override bool IsValid()
        {
            ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
    {
        public ApplyVoucherOrderValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.CodeVoucher)
                .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio");
        }
    }
}