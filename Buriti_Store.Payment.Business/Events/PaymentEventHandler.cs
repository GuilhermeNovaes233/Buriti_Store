using System.Threading;
using System.Threading.Tasks;
using Buriti_Store.Core.DomainObjects.DTO;
using Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents;
using MediatR;

namespace Buriti_Store.Payment.Business.Events
{
    public class PaymentEventHandler : INotificationHandler<OrderStockConfirmedEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(OrderStockConfirmedEvent message, CancellationToken cancellationToken)
        {
            var paymentOrder = new PaymentOrder
            {
                OrderId = message.OrderId,
                ClientId = message.ClientId,
                Total = message.Total,
                CardName = message.CardName,
                CardNumber = message.CardNumber,
                CardExpiration = message.CardExpiration,
                CardCvv = message.CardCvv
            };

            await _paymentService.MakePaymentOrder(paymentOrder);
        }
    }
}