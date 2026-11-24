using Documant.Context;
using Documant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;

        public DefaultController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // İletişim formu (AJAX): doğrulama hataları alan bazında JSON olarak döner.
        // Bind listesi ile SendDate / IsRead gibi alanların dışarıdan set edilmesi engellenir.
        [HttpPost]
        public IActionResult SendMessage([Bind("SenderName,SenderEmail,Subject,MessageDetail")] Message message)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

                return Json(new { success = false, errors });
            }

            message.SenderName = message.SenderName.Trim();
            message.SenderEmail = message.SenderEmail.Trim();
            message.Subject = message.Subject.Trim();
            message.MessageDetail = message.MessageDetail.Trim();
            message.SendDate = DateTime.Now;
            message.IsRead = false;

            _context.messages.Add(message);
            _context.SaveChanges();

            return Json(new { success = true, message = "Mesajınız başarıyla gönderildi. En kısa sürede dönüş yapacağım." });
        }
    }
}
