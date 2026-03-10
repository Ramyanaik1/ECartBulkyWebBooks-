using ECartBulkyWebBooks.Data;
using ECartBulkyWebBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECartBulkyWebBooks.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoriesController(ApplicationDbContext db)
        {
            _dbContext = db;
        }


        public IActionResult Index()
        {
            List<Category> categories = _dbContext.Categories.ToList();
            return View(categories);
        }
    }
}
