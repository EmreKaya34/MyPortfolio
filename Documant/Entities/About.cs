using System.ComponentModel.DataAnnotations;

namespace Documant.Entities
{
    public class About
    {
        public int AboutId { get; set; }

        [Required(ErrorMessage = "İsim zorunludur.")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "İsim 2-80 karakter arasında olmalıdır.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Unvan zorunludur.")]
        [StringLength(100, ErrorMessage = "Unvan en fazla 100 karakter olabilir.")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Konum zorunludur.")]
        [StringLength(80, ErrorMessage = "Konum en fazla 80 karakter olabilir.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tanıtım (1. paragraf) zorunludur.")]
        [StringLength(800, MinimumLength = 10, ErrorMessage = "Tanıtım 10-800 karakter arasında olmalıdır.")]
        public string Description1 { get; set; } = string.Empty;

        [StringLength(800, ErrorMessage = "2. paragraf en fazla 800 karakter olabilir.")]
        public string? Description2 { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Değer zorunludur.")]
        [StringLength(10, ErrorMessage = "En fazla 10 karakter.")]
        public string Stat1Value { get; set; } = string.Empty;
        [Required(ErrorMessage = "Etiket zorunludur.")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter.")]
        public string Stat1Label { get; set; } = string.Empty;

        [Required(ErrorMessage = "Değer zorunludur.")]
        [StringLength(10, ErrorMessage = "En fazla 10 karakter.")]
        public string Stat2Value { get; set; } = string.Empty;
        [Required(ErrorMessage = "Etiket zorunludur.")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter.")]
        public string Stat2Label { get; set; } = string.Empty;

        [Required(ErrorMessage = "Değer zorunludur.")]
        [StringLength(10, ErrorMessage = "En fazla 10 karakter.")]
        public string Stat3Value { get; set; } = string.Empty;
        [Required(ErrorMessage = "Etiket zorunludur.")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter.")]
        public string Stat3Label { get; set; } = string.Empty;
    }
}
