using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Documant.Areas.Admin.Models
{
    /// <summary>Proje ekleme / güncelleme formu (görsel yükleme ve kategori dropdown'ı için)</summary>
    public class ProjectFormViewModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Proje adı zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Proje adı 3-100 karakter arasında olmalıdır.")]
        public string ProjectTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proje açıklaması zorunludur.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Açıklama 10-1000 karakter arasında olmalıdır.")]
        public string ProjectDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "En az bir teknoloji giriniz.")]
        [StringLength(200, ErrorMessage = "Teknolojiler en fazla 200 karakter olabilir.")]
        public string Technologies { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public int CategoryId { get; set; }

        public bool IsActive { get; set; } = true;

        public string? ExistingImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
