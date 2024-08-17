using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> dbProductList = _unitOfWork.Product.GetAll().ToList();
            return View(dbProductList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            //if (obj.Name.ToLower() == "test")
            //{
            //	ModelState.AddModelError("", " Name != \"test\" ");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0)
            {
                return NotFound();
            }
            Product? dbProduct = _unitOfWork.Product.Get(u => u.Id == id);
            //Category? dbProduct2 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? dbProduct3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (dbProduct == null)
            {
                return NotFound();
            }
            return View(dbProduct);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.update(obj);
                _unitOfWork.save();
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                return NotFound();
            }
            Product? dbProduct = _unitOfWork.Product.Get(u => u.Id == id);
            //Category? dbProduct2 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? dbProduct3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (dbProduct == null)
            {
                return NotFound();
            }
            return View(dbProduct);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? dbProduct = _unitOfWork.Product.Get(u => u.Id == id);
            if (dbProduct == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(dbProduct);
            _unitOfWork.save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
