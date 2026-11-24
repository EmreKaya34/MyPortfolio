using Documant.Context;
using Documant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Documant.ViewCompanents.DefaultViewCompanents
{
    public class _DefaultFooterComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultFooterComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var model = new FooterViewModel
            {
                OwnerName = _context.Abouts.Select(x => x.FullName).FirstOrDefault() ?? "Portfolyo",
                Socials = _context.SocialMedias.Where(x => x.IsActive).OrderBy(x => x.SocialMediaId).ToList()
            };
            return View(model);
        }
    }
}
