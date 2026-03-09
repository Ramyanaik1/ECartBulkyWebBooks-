using Microsoft.AspNetCore.Mvc;

namespace ECartBulkyWebBooks.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
