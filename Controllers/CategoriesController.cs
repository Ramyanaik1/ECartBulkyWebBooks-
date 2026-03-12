
using ECartBulkyWebBooks.Data;
using ECartBulkyWebBooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();

            TempData["success"] = "Category created successfully";

            return RedirectToAction("Index");
           
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        // POST: Edit (Update)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();


                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");
            }

            return View(obj);
        }


        // GET: Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _dbContext.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _dbContext.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(obj);
            _dbContext.SaveChanges();


            TempData["success"] = "Category deleted successfully";


            return RedirectToAction("Index");
        }

        
    }
}
