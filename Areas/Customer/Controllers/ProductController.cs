
using System;
using System.Collections.Generic;   
using System.Linq;                  
using System.IO;                    

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;   

using ECartBulkyWebBooks.Data.Repository.IRepository;
using ECartBulkyWebBooks.Models;
using ECartBulkyWebBooks.Models.ViewModels;


namespace ECartBulkyWebBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // ✅ INDEX
        public IActionResult Index()
        {
            var objList = _unitOfWork.Product.GetAll();
            return View(objList);
        }

        // ✅ UPSERT (CREATE + EDIT)
        public IActionResult Upsert(int? id)
        {
            ProductVM vm = new ProductVM
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    })
            };

            if (id == null || id == 0)
                return View(vm);

            // 🔥 FIX: use correct Get method
            vm.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (vm.Product == null)
                return NotFound();

            return View(vm);
        }

        // ✅ POST UPSERT
        [HttpPost]
        public IActionResult Upsert(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(wwwRootPath, "images", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    vm.Product.ImageUrl = "/images/" + fileName;
                }

                if (vm.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(vm.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(vm.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product saved successfully!";
                return RedirectToAction("Index");
            }

            // 🔥 reload dropdown if validation fails
            vm.CategoryList = _unitOfWork.Category.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

            return View(vm);
        }

        // ✅ DELETE
        public IActionResult Delete(int? id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (obj == null) return NotFound();

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}