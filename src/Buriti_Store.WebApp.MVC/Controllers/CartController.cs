using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Buriti_Store.Orders.Application.Commands;
using Buriti_store.Catalog.Application.Interfaces;
using Buriti_Store.Core.Communication.Mediator;
using Buriti_Store.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Buriti_Store.Orders.Application.Queries.Interfaces;
using Buriti_Store.Orders.Application.Queries.ViewModels;

namespace Buriti_Store.WebApp.MVC.Controllers
{
    public class CartController : BaseController
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediator;
        private readonly IOrderQueries _orderQuery;

        public CartController(
            INotificationHandler<DomainNotification> notifications,
            IProductAppService productAppService, 
            IMediatorHandler mediator,
            IOrderQueries orderQuery) : base(notifications, mediator)
        {
            _orderQuery = orderQuery;
            _productAppService = productAppService;
            _mediator = mediator;
        }

        [Route("my-cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderQuery.GetCartClient(ClientId));
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

            TempData["Erro"] = GetMessageError();

            return RedirectToAction("ProductDetails", "Showcase", new { id });
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            var command = new RemoveOrderItemCommand(ClientId, id);
            await _mediator.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuery.GetCartClient(ClientId));
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantidade)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            var command = new UpdateOrderItemCommand(ClientId, id, quantidade);
            await _mediator.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuery.GetCartClient(ClientId));
        }

        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string codeVoucher)
        {
            var command = new ApplyVoucherOrderCommand(ClientId, codeVoucher);
            await _mediator.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQuery.GetCartClient(ClientId));
        }

        [Route("order-summary")]
        public async Task<IActionResult> OrderSummary()
        {
            return View(await _orderQuery.GetCartClient(ClientId));
        }

        [HttpPost]
        [Route("start-order")]
        public async Task<IActionResult> IniciarPedido(CartViewModel cartViewModel)
        {
            var cart = await _orderQuery.GetCartClient(ClientId);

            var command = new StartOrderCommand(cart.OrderId, ClientId, cart.TotalValue, cartViewModel.Payment.NameCard,
                cartViewModel.Payment.CardNumber, cartViewModel.Payment.ExpirationCard, cartViewModel.Payment.CvvCard);

            await _mediator.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index", "Order");
            }

            return View("PurchaseSummary", await _orderQuery.GetCartClient(ClientId));
        }
    }
}