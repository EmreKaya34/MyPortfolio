using System.ComponentModel.DataAnnotations;

namespace Documant.Entities
{
    public class HeroSection
    {
        public int HeroSectionId { get; set; }

        [Required(ErrorMessage = "Karşılama metni zorunludur.")]
        [StringLength(60, ErrorMessage = "Karşılama metni en fazla 60 karakter olabilir.")]
        public string Greeting { get; set; } = string.Empty;

        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Başlık 3-150 karakter arasında olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tanıtım metni zorunludur.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Tanıtım metni 10-500 karakter arasında olmalıdır.")]
        public string Description { get; set; } = string.Empty;

        public string? MainImageUrl { get; set; }
    }
}
