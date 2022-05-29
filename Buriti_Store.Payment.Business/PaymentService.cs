using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.DomainObjects.DTO;
using Buriti_Store.Core.Messages.CommonMessages.IntegrationEvents;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Buriti_Store.Payment.Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCardCreditFacade _paymentCardCreditFacade;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PaymentService(IPaymentCardCreditFacade paymentCardCreditFacade,
                                IPaymentRepository paymentRepository, 
                                IMediatorHandler mediatorHandler)
        {
            _paymentCardCreditFacade = paymentCardCreditFacade;
            _paymentRepository = paymentRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transaction> MakePaymentOrder(PaymentOrder paymentOrder)
        {
            var order = new Order
            {
                Id = paymentOrder.OrderId,
                Value = paymentOrder.Total
            };

            var payment = new Payment
            {
                Value = paymentOrder.Total,
                CardName = paymentOrder.CardName,
                CardNumber = paymentOrder.CardNumber,
                CardExpiration = paymentOrder.CardExpiration,
                CardCvv = paymentOrder.CardCvv,
                OrderId = paymentOrder.OrderId
            };

            var transaction = _paymentCardCreditFacade.MakePayment(order, payment);

            if (transaction.TransactionStatus == TransactionStatus.PaidOut)
            {
                payment.AddEvent(new OrderPaymentAccomplishedEvent(order.Id, paymentOrder.ClientId, transaction.PaymentId, transaction.Id, order.Value));

                _paymentRepository.Add(payment);
                _paymentRepository.AddTransaction(transaction);

                await _paymentRepository.UnitOfWork.Commit();
                return transaction;
            }

            await _mediatorHandler.PublishNotification(new DomainNotification("payment","A operadora recusou o payment"));
            await _mediatorHandler.PublishEvent(new RequestPaymentDeclinedEvent(order.Id, paymentOrder.ClientId, transaction.PaymentId, transaction.Id, order.Value));

            return transaction;
        }
    }
}