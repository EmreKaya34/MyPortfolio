using Documant.Context;
using Microsoft.AspNetCore.Mvc;

namespace Documant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        // Gelen kutusu: en yeniden eskiye, gönderen/konuya göre arama + durum filtresi
        public IActionResult MessageList(string? search, string? status, int? openId)
        {
            var query = _context.messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(x => x.SenderName.Contains(term) || x.Subject.Contains(term));
            }

            if (status == "unread") query = query.Where(x => !x.IsRead);
            else if (status == "read") query = query.Where(x => x.IsRead);

            ViewBag.Search = search;
            ViewBag.Status = status ?? "all";
            ViewBag.OpenId = openId; // Dashboard'dan gelirse modal otomatik açılır

            var values = query.OrderByDescending(x => x.SendDate).ToList();
            return View(values);
        }

        // Modal için mesaj detayı (JSON). Mesaj açıldığında okundu olarak işaretlenir.
        [HttpPost]
        public IActionResult OpenMessage(int id)
        {
            var message = _context.messages.Find(id);
            if (message == null) return NotFound();

            if (!message.IsRead)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }

            return Json(new
            {
                id = message.MessageId,
                senderName = message.SenderName,
                senderEmail = message.SenderEmail,
                subject = message.Subject,
                messageDetail = message.MessageDetail,
                sendDate = message.SendDate.ToString("dd MMMM yyyy, HH:mm"),
                isRead = message.IsRead,
                unreadCount = _context.messages.Count(x => !x.IsRead)
            });
        }

        // Okundu / Okunmadı durumunu tersine çevirir
        [HttpPost]
        public IActionResult ToggleRead(int id)
        {
            var message = _context.messages.Find(id);
            if (message == null) return NotFound();

            message.IsRead = !message.IsRead;
            _context.SaveChanges();

            return Json(new
            {
                isRead = message.IsRead,
                unreadCount = _context.messages.Count(x => !x.IsRead)
            });
        }

        [HttpPost]
        public IActionResult DeleteMessage(int id)
        {
            var message = _context.messages.Find(id);
            if (message != null)
            {
                _context.messages.Remove(message);
                _context.SaveChanges();
                TempData["Success"] = "Mesaj başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = "Silinecek mesaj bulunamadı.";
            }
            return RedirectToAction("MessageList");
        }
    }
}
