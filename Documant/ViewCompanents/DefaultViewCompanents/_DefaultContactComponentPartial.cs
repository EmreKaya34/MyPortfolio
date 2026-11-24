using Documant.Context;
using Documant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Documant.ViewCompanents.DefaultViewCompanents
{
    public class _DefaultContactComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultContactComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var model = new ContactSectionViewModel
            {
                Contact = _context.contacts.OrderBy(x => x.ContactId).FirstOrDefault(),
                Socials = _context.SocialMedias.Where(x => x.IsActive).OrderBy(x => x.SocialMediaId).ToList()
            };
            return View(model);
        }
    }
}
