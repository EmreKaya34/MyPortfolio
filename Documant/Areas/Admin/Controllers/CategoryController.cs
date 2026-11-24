using Documant.Context;
using Documant.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            var values = _context.Categories.Include(x => x.Projects).OrderBy(x => x.CategoryName).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View("CategoryForm", new Category());
        }

        [HttpPost]
        public IActionResult CreateCategory(Category model)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", model);

            _context.Categories.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "Kategori başarıyla eklendi.";
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null) return NotFound();
            return View("CategoryForm", value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category model)
        {
            if (!ModelState.IsValid)
                return View("CategoryForm", model);

            _context.Categories.Update(model);
            _context.SaveChanges();
            TempData["Success"] = "Kategori başarıyla güncellendi.";
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Silinecek kategori bulunamadı.";
                return RedirectToAction("CategoryList");
            }

            // Projesi olan kategori silinemez (ilişkisel bütünlük)
            if (_context.Projects.Any(x => x.CategoryId == id))
            {
                TempData["Error"] = "Bu kategoriye bağlı projeler var. Önce projeleri başka kategoriye taşıyın.";
                return RedirectToAction("CategoryList");
            }

            _context.Categories.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Kategori başarıyla silindi.";
            return RedirectToAction("CategoryList");
        }
    }
}
