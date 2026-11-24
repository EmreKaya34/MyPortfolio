using Documant.Context;
using Documant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Documant.ViewCompanents.DefaultViewCompanents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Pasif deneyimler sitede gösterilmez; en yeni kayıt en üstte
            var values = _context.experiences.Where(x => x.IsActive).OrderByDescending(x => x.ExperienceId).ToList();
            return View(values);
        }
    }
}
