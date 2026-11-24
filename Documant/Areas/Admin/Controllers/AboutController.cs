using Documant.Context;
using Documant.Entities;
using Documant.Services;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IImageService _imageService;

        public AboutController(PortfolioContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult UpdateAbout()
        {
            var value = _context.Abouts.FirstOrDefault() ?? new About();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(About model, IFormFile? photo)
        {
            var entity = _context.Abouts.FirstOrDefault();
            model.ImageUrl = entity?.ImageUrl;

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
                entity = new About();
                _context.Abouts.Add(entity);
            }

            entity.FullName = model.FullName.Trim();
            entity.JobTitle = model.JobTitle.Trim();
            entity.Location = model.Location.Trim();
            entity.Description1 = model.Description1.Trim();
            entity.Description2 = model.Description2?.Trim();
            entity.Stat1Value = model.Stat1Value; entity.Stat1Label = model.Stat1Label;
            entity.Stat2Value = model.Stat2Value; entity.Stat2Label = model.Stat2Label;
            entity.Stat3Value = model.Stat3Value; entity.Stat3Label = model.Stat3Label;

            var oldImage = entity.ImageUrl;
            if (newImage != null) entity.ImageUrl = newImage;

            _context.SaveChanges();
            if (newImage != null) _imageService.Delete(oldImage);

            TempData["Success"] = "Hakkımda bilgileri güncellendi.";
            return RedirectToAction("UpdateAbout");
        }
    }
}
