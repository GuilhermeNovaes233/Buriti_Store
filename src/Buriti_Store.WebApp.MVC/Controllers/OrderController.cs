using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using Buriti_Store.Orders.Application.Queries.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Buriti_Store.WebApp.MVC.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderQueries _orderQueries;

        public OrderController(
            IOrderQueries orderQueries,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _orderQueries = orderQueries;
        }

        [Route("my-orders")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderQueries.GetOrdersClient(ClientId));
        }
    }
}