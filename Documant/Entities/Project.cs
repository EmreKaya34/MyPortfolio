namespace Documant.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;

        // Veritabanında yalnızca dosya yolu tutulur (örn: /uploads/projects/abc.jpg)
        public string? ImageUrl { get; set; }

        // Virgülle ayrılmış teknoloji listesi (örn: "ASP.NET Core, EF Core, SQL Server")
        public string Technologies { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
