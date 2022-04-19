using System;
using System.Threading.Tasks;
using Buriti_store.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ShowcaseController : Controller
    {
        private readonly IProductAppService _productAppService;

        public ShowcaseController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("showcase")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            return View(await _productAppService.GetById(id));
        }
    }
}