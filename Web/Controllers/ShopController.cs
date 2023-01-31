using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ShopIndexVM model)
        {

            model = await _shopService.GetAllAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _shopService.GetAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TagProduct(int id)
        {
            var model = await _shopService.TagProductAsync(id);

            return PartialView("_TagProductPartial", model);

        }
    }
}
