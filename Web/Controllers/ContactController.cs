using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactIndexVM model)
        {
            var isSucceeded = await _contactService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
    }
}
