using Buriti_Store.Payment.Business;

namespace Buriti_Store.Payment.AntiCorruption
{
    public class PaymentCardCreditFacade : IPaymentCardCreditFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configManager;

        public PaymentCardCreditFacade(IPayPalGateway payPalGateway, IConfigurationManager configManager)
        {
            _payPalGateway = payPalGateway;
            _configManager = configManager;
        }

        public Transaction MakePayment(Order order, Business.Payment payment)
        {
            var apiKey = _configManager.GetValue("apiKey");
            var encriptionKey = _configManager.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);

            var pagamentoResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Value);

            // TODO: O gateway de pagamentos que deve retornar o objeto transação
            var transaction = new Transaction
            {
                OrderId = order.Id,
                Amount = order.Value,
                PaymentId = payment.Id
            };

            if (pagamentoResult)
            {
                transaction.TransactionStatus = TransactionStatus.PaidOut;
                return transaction;
            }

            transaction.TransactionStatus = TransactionStatus.Refused;
            return transaction;
        }
    }
}