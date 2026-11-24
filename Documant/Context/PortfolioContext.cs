using Documant.Entities;
using Microsoft.EntityFrameworkCore;

namespace Documant.Context
{
    public class PortfolioContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OVVJLQ1;Database=PortFolioDayDb;integrated security=true; Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Projesi olan bir kategori silinirse projeler de zincirleme silinmesin
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Experience> experiences { get; set; }//sql tarafı çoğul c# tarafı tekil
        public DbSet<HeroSection> heroSections { get; set; }
        public DbSet<HeroTitle> heroTitles { get; set; }
        public DbSet<Message> messages { get; set; }

        public DbSet<Contact> contacts { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        public DbSet<Skill> skills { get; set; }
        public DbSet<SkillItem> SkillItems { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Education> Educations { get; set; }
    }
}
