using System.ComponentModel.DataAnnotations;

namespace Documant.Entities
{
    public class Education
    {
        public int EducationId { get; set; }

        [Required(ErrorMessage = "Bölüm / derece zorunludur.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Bölüm 2-150 karakter arasında olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Okul adı zorunludur.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Okul adı 2-150 karakter arasında olmalıdır.")]
        public string SchoolName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Açıklama 10-500 karakter arasında olmalıdır.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rozet metni zorunludur.")]
        [StringLength(30, ErrorMessage = "Rozet en fazla 30 karakter olabilir.")]
        public string Badge { get; set; } = "Mezuniyet";

        [Range(1, 100, ErrorMessage = "Sıra 1-100 arasında olmalıdır.")]
        public int DisplayOrder { get; set; } = 1;
    }
}
