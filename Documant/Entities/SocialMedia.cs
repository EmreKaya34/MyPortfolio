using System.ComponentModel.DataAnnotations;

namespace Documant.Entities
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }

        [Required(ErrorMessage = "Platform adı zorunludur.")]
        [StringLength(40, ErrorMessage = "Platform adı en fazla 40 karakter olabilir.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bağlantı adresi zorunludur.")]
        [Url(ErrorMessage = "Geçerli bir URL giriniz (https://...).")]
        [StringLength(250, ErrorMessage = "URL en fazla 250 karakter olabilir.")]
        public string Url { get; set; } = string.Empty;

        // Bootstrap Icons sınıfı (örn: "bi bi-github")
        [Required(ErrorMessage = "İkon sınıfı zorunludur.")]
        [StringLength(40, ErrorMessage = "İkon sınıfı en fazla 40 karakter olabilir.")]
        public string IconClass { get; set; } = "bi bi-link-45deg";

        public bool IsActive { get; set; } = true;
    }
}
