using Buriti_Store.Core.Data;

namespace Buriti_Store.Payment.Business
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Add(Payment payment);

        void AddTransaction(Transaction transaction);
    }
}