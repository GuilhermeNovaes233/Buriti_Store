using Buriti_Store.Core.Messages;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Buriti_Store.Orders.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
    {
        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            return true;
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
