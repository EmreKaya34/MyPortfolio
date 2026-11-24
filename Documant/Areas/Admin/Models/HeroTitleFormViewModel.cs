using System.ComponentModel.DataAnnotations;

namespace Documant.Areas.Admin.Models
{
    /// <summary>Ana sayfadaki daktilo animasyonunda dönen unvanların ekleme / güncelleme formu</summary>
    public class HeroTitleFormViewModel
    {
        public int HeroTitleId { get; set; }

        [Required(ErrorMessage = "Unvan zorunludur.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Unvan 2-60 karakter arasında olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Sıra 1-100 arasında olmalıdır.")]
        public int Order { get; set; } = 1;

        public bool IsActive { get; set; } = true;
    }
}
