using Documant.Areas.Admin.Models;
using Documant.Context;
using Documant.Entities;
using Documant.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private const string ImageFolder = "projects";

        private readonly PortfolioContext _context;
        private readonly IImageService _imageService;
        private string? _uploadedImagePath;

        public ProjectController(PortfolioContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // Admin panelinde aktif + pasif tüm projeler görünür
        public IActionResult ProjectList(int? categoryId, string? sort)
        {
            var query = _context.Projects.Include(x => x.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            query = sort switch
            {
                "oldest" => query.OrderBy(x => x.CreatedDate),
                "title" => query.OrderBy(x => x.ProjectTitle),
                _ => query.OrderByDescending(x => x.CreatedDate)
            };

            ViewBag.Categories = new SelectList(GetCategoryItems(), "Value", "Text", categoryId?.ToString());
            ViewBag.Sort = sort ?? "newest";
            ViewBag.IsFiltered = categoryId.HasValue;
            return View(query.ToList());
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View("ProjectForm", new ProjectFormViewModel { Categories = GetCategoryItems() });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectFormViewModel model)
        {
            model.Categories = GetCategoryItems();
            await ValidateCategoryAndImageAsync(model);

            if (!ModelState.IsValid)
            {
                _imageService.Delete(_uploadedImagePath); // Form hatalıysa yüklenen dosya sunucuda kalmasın
                return View("ProjectForm", model);
            }

            var project = new Project { CreatedDate = DateTime.Now };
            MapToEntity(model, project);
            project.ImageUrl = _uploadedImagePath;

            _context.Projects.Add(project);
            _context.SaveChanges();

            TempData["Success"] = "Proje başarıyla eklendi.";
            return RedirectToAction("ProjectList");
        }

        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null) return NotFound();

            var model = new ProjectFormViewModel
            {
                ProjectId = project.ProjectId,
                ProjectTitle = project.ProjectTitle,
                ProjectDescription = project.ProjectDescription,
                Technologies = project.Technologies,
                CategoryId = project.CategoryId,
                IsActive = project.IsActive,
                ExistingImageUrl = project.ImageUrl,
                Categories = GetCategoryItems()
            };
            return View("ProjectForm", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(ProjectFormViewModel model)
        {
            var project = _context.Projects.Find(model.ProjectId);
            if (project == null) return NotFound();

            model.Categories = GetCategoryItems();
            model.ExistingImageUrl = project.ImageUrl;
            await ValidateCategoryAndImageAsync(model);

            if (!ModelState.IsValid)
            {
                _imageService.Delete(_uploadedImagePath); // Form hatalıysa yüklenen dosya sunucuda kalmasın
                return View("ProjectForm", model);
            }

            MapToEntity(model, project);

            var oldImage = project.ImageUrl;
            if (_uploadedImagePath != null)
                project.ImageUrl = _uploadedImagePath;

            _context.SaveChanges();

            // Yeni görsel yüklendiyse eski dosya diskten silinir
            if (_uploadedImagePath != null)
                _imageService.Delete(oldImage);

            TempData["Success"] = "Proje başarıyla güncellendi.";
            return RedirectToAction("ProjectList");
        }

        [HttpPost]
        public IActionResult ToggleProjectStatus(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                TempData["Error"] = "Proje bulunamadı.";
                return RedirectToAction("ProjectList");
            }

            project.IsActive = !project.IsActive;
            _context.SaveChanges();
            TempData["Success"] = project.IsActive
                ? "Proje aktif edildi, sitede görünecek."
                : "Proje pasif yapıldı, sitede gizlendi.";
            return RedirectToAction("ProjectList");
        }

        [HttpPost]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                TempData["Error"] = "Silinecek proje bulunamadı.";
                return RedirectToAction("ProjectList");
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();
            _imageService.Delete(project.ImageUrl);

            TempData["Success"] = "Proje başarıyla silindi.";
            return RedirectToAction("ProjectList");
        }

        // ---------- Yardımcı metotlar ----------

        private List<SelectListItem> GetCategoryItems()
        {
            return _context.Categories
                .OrderBy(x => x.CategoryName)
                .Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName })
                .ToList();
        }

        private async Task ValidateCategoryAndImageAsync(ProjectFormViewModel model)
        {
            if (model.CategoryId > 0 && !_context.Categories.Any(x => x.CategoryId == model.CategoryId))
                ModelState.AddModelError(nameof(model.CategoryId), "Seçilen kategori bulunamadı.");

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var result = await _imageService.SaveAsync(model.ImageFile, ImageFolder);
                if (result.Success)
                    _uploadedImagePath = result.Path;
                else
                    ModelState.AddModelError(nameof(model.ImageFile), result.Error!);
            }
        }

        private static void MapToEntity(ProjectFormViewModel model, Project project)
        {
            project.ProjectTitle = model.ProjectTitle.Trim();
            project.ProjectDescription = model.ProjectDescription.Trim();
            project.Technologies = model.Technologies.Trim();
            project.CategoryId = model.CategoryId;
            project.IsActive = model.IsActive;
        }
    }
}
