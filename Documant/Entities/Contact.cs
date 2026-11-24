using System.ComponentModel.DataAnnotations;

namespace Documant.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Başlık 2-100 karakter arasında olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Açıklama 10-500 karakter arasında olmalıdır.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lokasyon zorunludur.")]
        [StringLength(100, ErrorMessage = "Lokasyon en fazla 100 karakter olabilir.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(20, ErrorMessage = "Telefon en fazla 20 karakter olabilir.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
