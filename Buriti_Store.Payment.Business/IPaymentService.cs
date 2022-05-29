using Buriti_Store.Core.DomainObjects.DTO;
using System.Threading.Tasks;

namespace Buriti_Store.Payment.Business
{
    public interface IPaymentService
    {
        Task<Transaction> MakePaymentOrder(PaymentOrder paymentOrder);
    }
}