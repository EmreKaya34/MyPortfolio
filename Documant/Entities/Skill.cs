using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Documant.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Yetenek adı zorunludur.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Yetenek adı 2-60 karakter arasında olmalıdır.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Açıklama 5-200 karakter arasında olmalıdır.")]
        public string Description { get; set; } = string.Empty;

        // Bootstrap Icons sınıfı tutulur (örn: "bi bi-server")
        [Required(ErrorMessage = "İkon sınıfı zorunludur.")]
        [StringLength(50, ErrorMessage = "İkon sınıfı en fazla 50 karakter olabilir.")]
        public string IconUrl { get; set; } = "bi bi-code-slash";

        [Range(0, 100, ErrorMessage = "Seviye 0 ile 100 arasında olmalıdır.")]
        public int Level { get; set; }

        [ValidateNever]
        public List<SkillItem> SkillItems { get; set; } = new();
    }
}
