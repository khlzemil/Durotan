using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(BlogIndexVM model)
        {
            model = await _blogService.GetAllAsync(model);
            return View(model);
        }
    }
}
