namespace Documant.Areas.Admin.Models
{
    /// <summary>Liste/form sayfalarındaki ortak başlık alanı için model (_PageHeader)</summary>
    public record PageHeaderModel(string Title, string Subtitle, string? ButtonText = null, string? ButtonAction = null);
}
