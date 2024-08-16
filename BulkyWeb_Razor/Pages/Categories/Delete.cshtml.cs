using BulkyWeb_Razor.Data;
using BulkyWeb_Razor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
		private readonly ApplicationDbContext _db;
		[BindProperty]
		public Category Category { get; set; }
		public DeleteModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int? id)
        {
			if (id != null | id != 0)
			{
				Category = _db.Categories.Find(id);
			}
			
		}
		public IActionResult OnPost(int? id)
		{
			Category? dbCategory = _db.Categories.Find(Category.Id);
			if (dbCategory == null)
			{
				return NotFound();
			}
			_db.Categories.Remove(dbCategory);
			_db.SaveChanges();
			TempData["success"] = "Category Deleted Successfully";
			return RedirectToPage("Index");
		}
	}
}
