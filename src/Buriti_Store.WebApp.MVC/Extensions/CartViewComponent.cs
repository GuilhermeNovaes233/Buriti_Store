using Buriti_Store.Orders.Application.Queries.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Buriti_Store.WebApp.MVC.Extensions
{
    public class CartViewComponent
    {
        private readonly IOrderQueries _orderQueries;

        // TODO: Obter cliente logado
        protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        public CartViewComponent(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _orderQueries.GetCartClient(ClientId);
            var itens = cart?.Items.Count ?? 0;

            //Todo - implementar
            return null;// View(itens.ToString());
        }
    }
}
