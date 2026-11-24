using Documant.Context;
using Documant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ContactList()
        {
            var values = _context.contacts.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View("ContactForm", new Contact());
        }

        [HttpPost]
        public IActionResult CreateContact(Contact model)
        {
            if (!ModelState.IsValid)
                return View("ContactForm", model);

            _context.contacts.Add(model);
            _context.SaveChanges();
            TempData["Success"] = "İletişim bilgisi başarıyla eklendi.";
            return RedirectToAction("ContactList");
        }

        [HttpGet]
        public IActionResult UpdateContact(int id)
        {
            var value = _context.contacts.Find(id);
            if (value == null) return NotFound();
            return View("ContactForm", value);
        }

        [HttpPost]
        public IActionResult UpdateContact(Contact model)
        {
            if (!ModelState.IsValid)
                return View("ContactForm", model);

            _context.contacts.Update(model);
            _context.SaveChanges();
            TempData["Success"] = "İletişim bilgisi başarıyla güncellendi.";
            return RedirectToAction("ContactList");
        }

        [HttpPost]
        public IActionResult DeleteContact(int id)
        {
            var value = _context.contacts.Find(id);
            if (value == null)
            {
                TempData["Error"] = "Silinecek kayıt bulunamadı.";
                return RedirectToAction("ContactList");
            }

            _context.contacts.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "İletişim bilgisi başarıyla silindi.";
            return RedirectToAction("ContactList");
        }
    }
}
