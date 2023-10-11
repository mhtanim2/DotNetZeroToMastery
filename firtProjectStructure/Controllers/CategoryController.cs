using firtProjectStructure.Data;
using firtProjectStructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Remoting;

namespace firtProjectStructure.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _db;
        public CategoryController(DataContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> obj=_db.Categories;
            return View(obj);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
				return NotFound();
			var data = _db.Categories.Find(id);
			if (data == null)
				return NotFound();

			return View(data);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}


		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var categoryFromDb = _db.Categories.Find(id);

			if (categoryFromDb == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		//POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _db.Categories.Find(id);
			if (obj == null)
			{
				return NotFound();
			}

			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");

		}
	}
}
