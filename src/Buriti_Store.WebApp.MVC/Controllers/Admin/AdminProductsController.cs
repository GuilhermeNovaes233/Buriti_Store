using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Buriti_store.Catalog.Application.ViewModels;
using Buriti_store.Catalog.Application.Interfaces;

namespace Buriti_store.WebApp.MVC.Controllers.Admin
{
    public class AdminProductsController : Controller
    {
        private readonly IProductAppService _productAppService;

        public AdminProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("admin-Products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await GetCategories(new ProductViewModel()));
        }

        [Route("new-product")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductViewModel ProductViewModel)
        {
            if (!ModelState.IsValid) return View(await GetCategories(ProductViewModel));

            await _productAppService.AddProduct(ProductViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await GetCategories(await _productAppService.GetById(id)));
        }

        [HttpPost]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel)
        {
            var product = await _productAppService.GetById(id);
            productViewModel.QuantityStock = product.QuantityStock;

            ModelState.Remove("QuantityStock");
            if (!ModelState.IsValid) return View(await GetCategories(productViewModel));

            await _productAppService.UpdateProduct(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productAppService.GetById(id));
        }

        [HttpPost]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity)
        {
            if (quantity > 0)
            {
                await _productAppService.ReplenishStock(id, quantity);
            }
            else
            {
                await _productAppService.DebitStock(id, quantity);
            }

            return View("Index", await _productAppService.GetAll());
        }

        private async Task<ProductViewModel> GetCategories(ProductViewModel product)
        {
            product.Categories = await _productAppService.GetCategories();
            return product;
        }
    }
}