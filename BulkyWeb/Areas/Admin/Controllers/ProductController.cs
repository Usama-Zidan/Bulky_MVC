using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> dbProductList = _unitOfWork.Product.GetAll(includeProps:"Category").ToList();
            
            return View(dbProductList);
        }
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
			//.GetAll().Select(u => new SelectListItem
			//{
			//	Text = u.Name,
			//	Value = u.Id.ToString()
			//});

            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryLists"] = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category
				.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
                Product = new Product()
            };
            if (id == null || id ==0)
            {
				return View(productVM);
			}
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

			
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM , IFormFile? img)
        {
            //if (obj.Name.ToLower() == "test")
            //{
            //	ModelState.AddModelError("", " Name != \"test\" ");
            //}
            if (ModelState.IsValid)
            {
                string wwwRootPth = _webHostEnvironment.WebRootPath;
                if (img != null)
                {
                    string fileName = Guid.NewGuid().ToString()+ Path.GetExtension(img.FileName);
                    string productPth = Path.Combine(wwwRootPth, @"images\product");

                    if (!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                    {
                        var oldimgpth = Path.Combine(wwwRootPth, ProductVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimgpth))
                        {
                            System.IO.File.Delete(oldimgpth);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPth, fileName) ,FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                    ProductVM.Product.ImageUrl = @"\images\product\" + fileName;

				}
                if(ProductVM.Product.Id == 0)
                {
					_unitOfWork.Product.Add(ProductVM.Product);
				}
                else
                {
                    _unitOfWork.Product.update(ProductVM.Product);
                }
                
                _unitOfWork.save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ProductVM.CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
			}
            return View(ProductVM);
        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null | id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? dbProduct = _unitOfWork.Product.Get(u => u.Id == id);
        //    //Category? dbProduct2 = _db.Categories.FirstOrDefault(u=>u.Id == id);
        //    //Category? dbProduct3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        //    if (dbProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(dbProduct);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.update(obj);
        //        _unitOfWork.save();
        //        TempData["success"] = "Product Updated Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null | id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? dbProduct = _unitOfWork.Product.Get(u => u.Id == id);
        //    //Category? dbProduct2 = _db.Categories.FirstOrDefault(u=>u.Id == id);
        //    //Category? dbProduct3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        //    if (dbProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(dbProduct);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{
        //    Product? dbProduct = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (dbProduct == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(dbProduct);
        //    _unitOfWork.save();
        //    TempData["success"] = "Product Deleted Successfully";
        //    return RedirectToAction("Index");
        //}

        #region Api Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> dbProductList = _unitOfWork.Product.GetAll(includeProps: "Category").ToList();
            return Json(new { data = dbProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productedToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productedToBeDeleted == null)
            {
                return Json(new { success = false  , message="error while deleting"});
            }
            var oldimgpth = Path.Combine(_webHostEnvironment.WebRootPath, productedToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldimgpth))
            {
                System.IO.File.Delete(oldimgpth);
            }
            _unitOfWork.Product.Remove(productedToBeDeleted);
            _unitOfWork.save();
            List<Product> dbProductList = _unitOfWork.Product.GetAll(includeProps: "Category").ToList();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
