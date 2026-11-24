using System.ComponentModel.DataAnnotations;

namespace Documant.Entities
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Ad soyad 2-80 karakter arasında olmalıdır.")]
        public string SenderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(120, ErrorMessage = "E-posta en fazla 120 karakter olabilir.")]
        public string SenderEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Konu zorunludur.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Konu 3-150 karakter arasında olmalıdır.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Mesaj 10-2000 karakter arasında olmalıdır.")]
        public string MessageDetail { get; set; } = string.Empty;

        public DateTime SendDate { get; set; }

        public bool IsRead { get; set; }
    }
}
