using Buriti_Store.Core.Data;
using Buriti_Store.Payment.Business;

namespace Buriti_Store.Payments.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;


        public void Add(Payment.Business.Payment payment)
        {
            _context.Payments.Add(payment);
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}