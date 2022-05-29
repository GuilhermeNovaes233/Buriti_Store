using System;
using Buriti_Store.Core.Messages;
using FluentValidation;

namespace Buriti_Store.Orders.Application.Commands
{
    public class RemoveOrderItemCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }

        public RemoveOrderItemCommand(Guid clientId, Guid productId)
        {
            ClientId = clientId;
            ProductId = productId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoveOrderItemValidation : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    }
}