using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            var isSucceeded = await _productService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            await _productService.CreateAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var model = await _productService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, ProductUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _productService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _productService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _productService.DeleteAsync(id);
            if (isSucceded)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id, int aboutUsId)
        {
            var isSucceded = await _productService.DeletePhotoAsync(id);
            if (isSucceded) return RedirectToAction("update", "aboutus", new { id = aboutUsId });

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePhoto(int id)
        {
            var model = await _productService.GetPhotoUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(int id, ProductPhotoUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            var isSucceeded = await _productService.UpdatePhotoAsync(model);
            if (isSucceeded) return RedirectToAction("update", "product", new { id = model.ProductId });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddTags(int id)
        {
            var model = await _productService.GetAddTagsModelAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTags(int id, ProductAddTagsVM model)
        {
            if (id != model.ProductId) return BadRequest();
            var isSucceeded = await _productService.AddTagsAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            model = await _productService.GetAddTagsModelAsync(model.ProductId);
            return View(model);
        }
    }
}
