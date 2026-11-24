using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Documant.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Kategori adı 2-60 karakter arasında olmalıdır.")]
        public string CategoryName { get; set; } = string.Empty;

        [ValidateNever]
        public List<Project> Projects { get; set; } = new();
    }
}
