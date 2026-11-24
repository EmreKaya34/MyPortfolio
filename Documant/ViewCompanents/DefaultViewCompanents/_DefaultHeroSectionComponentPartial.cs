using Documant.Context;
using Documant.Models;
using Microsoft.AspNetCore.Mvc;

namespace Documant.ViewCompanents.DefaultViewCompanents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var hero = _context.heroSections.FirstOrDefault();
            var about = _context.Abouts.FirstOrDefault();

            // Daktilo animasyonu: önce Hakkımda unvanı, ardından Admin > Dönen Unvanlar
            var rotatingTitles = _context.heroTitles
                .Where(x => x.IsActive)
                .OrderBy(x => x.Order)
                .Select(x => x.Title)
                .ToList();
            if (!string.IsNullOrWhiteSpace(about?.JobTitle))
                rotatingTitles.Insert(0, about.JobTitle);
            rotatingTitles = rotatingTitles.Distinct().ToList();

            // Veritabanı boşsa bile sayfa hata vermesin diye varsayılan değerler kullanılır
            var model = new HeroViewModel
            {
                Greeting = hero?.Greeting ?? "Merhaba, ben",
                Title = hero?.Title ?? about?.FullName ?? "Portfolyo",
                Description = hero?.Description ?? string.Empty,
                ImageUrl = hero?.MainImageUrl ?? about?.ImageUrl,
                RotatingTitles = rotatingTitles,
                Stat1Value = about?.Stat1Value ?? string.Empty,
                Stat1Label = about?.Stat1Label ?? string.Empty,
                Stat3Value = about?.Stat3Value ?? string.Empty,
                Stat3Label = about?.Stat3Label ?? string.Empty,
                Socials = _context.SocialMedias.Where(x => x.IsActive).OrderBy(x => x.SocialMediaId).ToList()
            };
            return View(model);
        }
    }
}
