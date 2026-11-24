using System.ComponentModel.DataAnnotations;

namespace Documant.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Required(ErrorMessage = "Unvan zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Unvan 2-100 karakter arasında olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Şirket adı 2-100 karakter arasında olmalıdır.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Açıklama 10-1000 karakter arasında olmalıdır.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çalışma türü zorunludur.")]
        [StringLength(50, ErrorMessage = "Çalışma türü en fazla 50 karakter olabilir.")]
        public string CategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
