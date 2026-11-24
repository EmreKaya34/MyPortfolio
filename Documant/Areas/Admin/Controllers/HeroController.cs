using Documant.Context;
using Documant.Entities;
using Documant.Services;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IImageService _imageService;

        public HeroController(PortfolioContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // Tekil içerik: liste yerine doğrudan güncelleme ekranı
        [HttpGet]
        public IActionResult UpdateHero()
        {
            var value = _context.heroSections.FirstOrDefault() ?? new HeroSection();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHero(HeroSection model, IFormFile? photo)
        {
            var entity = _context.heroSections.FirstOrDefault();
            model.MainImageUrl = entity?.MainImageUrl;

            string? newImage = null;
            if (photo != null && photo.Length > 0)
            {
                var result = await _imageService.SaveAsync(photo, "profile");
                if (result.Success) newImage = result.Path;
                else ModelState.AddModelError("photo", result.Error!);
            }

            if (!ModelState.IsValid)
            {
                _imageService.Delete(newImage); // Form hatalıysa yüklenen dosya sunucuda kalmasın
                return View(model);
            }

            if (entity == null)
            {
                entity = new HeroSection();
                _context.heroSections.Add(entity);
            }

            entity.Greeting = model.Greeting.Trim();
            entity.Title = model.Title.Trim();
            entity.Description = model.Description.Trim();

            var oldImage = entity.MainImageUrl;
            if (newImage != null) entity.MainImageUrl = newImage;

            _context.SaveChanges();
            if (newImage != null) _imageService.Delete(oldImage);

            TempData["Success"] = "Ana sayfa bilgileri güncellendi.";
            return RedirectToAction("UpdateHero");
        }
    }
}
