using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> dbCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(dbCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if (obj.Name.ToLower() == "test")
            //{
            //	ModelState.AddModelError("", " Name != \"test\" ");
            //}

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", " Name != Order ");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.save();
                TempData["success"] = "Category Created Successfully";
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
            Category? dbCategory = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? dbCategory2 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? dbCategory3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (dbCategory == null)
            {
                return NotFound();
            }
            return View(dbCategory);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.update(obj);
                _unitOfWork.save();
                TempData["success"] = "Category Updated Successfully";
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
            Category? dbCategory = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? dbCategory2 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? dbCategory3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (dbCategory == null)
            {
                return NotFound();
            }
            return View(dbCategory);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? dbCategory = _unitOfWork.Category.Get(u => u.Id == id);
            if (dbCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(dbCategory);
            _unitOfWork.save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
