using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Buriti_Store.Orders.Application.Commands;
using Buriti_store.Catalog.Application.Interfaces;
using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace Buriti_Store.WebApp.MVC.Controllers
{
    public class CartController : BaseController
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediator;

        public CartController(
            INotificationHandler<DomainNotification> notifications,
            IProductAppService productAppService, 
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _productAppService = productAppService;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            if (product.QuantityStock < quantity)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProductDetails", "Showcase", new { id });
            }

            var command = new AddOrderItemCommand(ClientId, product.Id, product.Name, quantity, product.Value);

            await _mediator.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            TempData["Erros"] = GetMessageError();

            return RedirectToAction("ProductDetails", "Showcase", new { id });
        }
    }
}
