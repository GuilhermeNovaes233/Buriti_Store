namespace Buriti_Store.Payment.Business
{
    public interface IPaymentCardCreditFacade
    {
        Transaction MakePayment(Order order, Payment payment);
    }
}