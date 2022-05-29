using System;
using Buriti_Store.Core.Messages;
using FluentValidation;


namespace Buriti_Store.Orders.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Amount { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpirationCard { get; private set; }
        public string CvvCard { get; private set; }

        public StartOrderCommand(Guid orderId, Guid clientId, decimal amount, string cardName, string cardNumber, string expirationCard, string cvvCard)
        {
            OrderId = orderId;
            ClientId = clientId;
            Amount = amount;
            CardName = cardName;
            CardNumber = cardNumber;
            ExpirationCard = expirationCard;
            CvvCard = cvvCard;
        }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido");

            RuleFor(c => c.CardName)
                .NotEmpty()
                .WithMessage("O nome no cartão não foi informado");

            RuleFor(c => c.CardNumber)
                .CreditCard()
                .WithMessage("Número de cartão de crédito inválido");

            RuleFor(c => c.ExpirationCard)
                .NotEmpty()
                .WithMessage("Data de expiração não informada");

            RuleFor(c => c.CvvCard)
                .Length(3, 4)
                .WithMessage("O CVV não foi preenchido corretamente");
        }
    }
}