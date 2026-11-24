using Documant.Context;
using Documant.Entities;

namespace Documant.Services
{
    /// <summary>
    /// Uygulama açılırken boş tablolara örnek veri ekler. Dolu tablolara dokunmaz,
    /// her çalıştırmada tekrar eklemez. Tüm içerik Admin panelinden değiştirilebilir.
    /// </summary>
    public static class DbSeeder
    {
        private const string ProfileImage = "/uploads/profile/profil.jpg";

        public static void Seed(PortfolioContext db)
        {
            // Ana sayfa (Hero)
            var hero = db.heroSections.FirstOrDefault();
            if (hero == null)
            {
                db.heroSections.Add(new HeroSection
                {
                    Greeting = "Merhaba, ben",
                    Title = "Emre Kaya",
                    Description = "ASP.NET Core ile modern, temiz ve ölçeklenebilir web uygulamaları geliştiriyorum. Öğrenmeyi ve ürettiklerimi paylaşmayı seviyorum.",
                    MainImageUrl = ProfileImage
                });
            }
            else if (string.IsNullOrEmpty(hero.MainImageUrl))
            {
                hero.MainImageUrl = ProfileImage;
            }

            // Hakkımda
            if (!db.Abouts.Any())
            {
                db.Abouts.Add(new About
                {
                    FullName = "Emre Kaya",
                    JobTitle = "Yazılım Geliştirici",
                    Location = "İstanbul",
                    Description1 = "Merhaba, ben Emre. .NET ekosisteminde web uygulamaları geliştiriyorum. ASP.NET Core MVC, Entity Framework Core ve SQL Server ile çalışmayı seviyorum.",
                    Description2 = "Yazılımın yalnızca kod yazmak olmadığına, problem çözmek ve değer üretmek olduğuna inanıyorum. Yeni teknolojileri öğrenmeye ve projelerimde uygulamaya devam ediyorum.",
                    ImageUrl = ProfileImage,
                    Stat1Value = "6+", Stat1Label = "Proje",
                    Stat2Value = "10+", Stat2Label = "Teknoloji",
                    Stat3Value = "100%", Stat3Label = "Öğrenme Tutkusu"
                });
            }

            // Yetenekler
            if (!db.skills.Any())
            {
                db.skills.AddRange(
                    new Skill { Title = "C# / ASP.NET Core", Description = "MVC mimarisi, katmanlı yapı ve temiz kod ile web uygulamaları.", IconUrl = "bi bi-server", Level = 85 },
                    new Skill { Title = "Entity Framework Core", Description = "Code First, migration, ilişkiler ve LINQ sorguları.", IconUrl = "bi bi-diagram-3", Level = 80 },
                    new Skill { Title = "SQL Server", Description = "İlişkisel veritabanı tasarımı, sorgular ve optimizasyon.", IconUrl = "bi bi-database", Level = 75 },
                    new Skill { Title = "HTML / CSS / Bootstrap", Description = "Duyarlı, erişilebilir ve modern kullanıcı arayüzleri.", IconUrl = "bi bi-layout-wtf", Level = 80 },
                    new Skill { Title = "JavaScript", Description = "DOM işlemleri, AJAX ve etkileşimli arayüz bileşenleri.", IconUrl = "bi bi-braces", Level = 65 },
                    new Skill { Title = "Git & GitHub", Description = "Sürüm kontrolü, dal yönetimi ve ekip çalışması.", IconUrl = "bi bi-git", Level = 70 });
            }

            // Deneyim
            if (!db.experiences.Any())
            {
                db.experiences.AddRange(
                    new Experience { Title = "Yazılım Geliştirme Stajyeri", CompanyName = "Teknoloji Şirketi", CategoryName = "Staj", IsActive = true,
                        Description = "ASP.NET Core ile kurumsal web uygulamalarının geliştirilmesine katkı verdim, veritabanı sorguları ve arayüz iyileştirmeleri üzerinde çalıştım." },
                    new Experience { Title = ".NET Eğitim Programı Katılımcısı", CompanyName = "Yazılım Akademisi", CategoryName = "Eğitim", IsActive = true,
                        Description = "C#, ASP.NET Core MVC, Entity Framework Core ve SQL Server konularında proje tabanlı yoğun bir eğitim programı tamamladım." });
            }

            // Eğitim
            if (!db.Educations.Any())
            {
                db.Educations.AddRange(
                    new Education { Title = "Yazılım Geliştirme Eğitimi — .NET", SchoolName = "Yazılım Akademisi", Badge = "Devam Ediyor", DisplayOrder = 1,
                        Description = "ASP.NET Core MVC, Entity Framework Core, SQL Server ve modern web geliştirme üzerine proje tabanlı eğitim." },
                    new Education { Title = "Önlisans — Bilgisayar Programcılığı", SchoolName = "Üniversite", Badge = "Mezuniyet", DisplayOrder = 2,
                        Description = "Programlama temelleri, algoritmalar, veritabanı ve web teknolojileri üzerine temel eğitim." });
            }

            // İletişim
            if (!db.contacts.Any())
            {
                db.contacts.Add(new Contact
                {
                    Title = "Harika Bir Fikrin mi Var? Birlikte Hayata Geçirelim.",
                    Description = "Yeni projeler, iş birlikleri veya yalnızca teknoloji üzerine sohbet etmek için benimle iletişime geçebilirsin.",
                    Email = "emre@example.com",
                    PhoneNumber = "+90 555 000 00 00",
                    Location = "İstanbul, Türkiye"
                });
            }

            // Sosyal medya
            if (!db.SocialMedias.Any())
            {
                db.SocialMedias.AddRange(
                    new SocialMedia { Name = "GitHub", Url = "https://github.com/EmreKaya34", IconClass = "bi bi-github", IsActive = true },
                    new SocialMedia { Name = "LinkedIn", Url = "https://www.linkedin.com/in/emre-kaya-034612323/", IconClass = "bi bi-linkedin", IsActive = true },
                    new SocialMedia { Name = "Instagram", Url = "https://www.instagram.com/eemrekaya034/", IconClass = "bi bi-instagram", IsActive = true });
            }

            db.SaveChanges();

            // Kategoriler + projeler: yalnızca ikisi de boşsa eklenir.
            // Böylece Admin panelinden silinen kayıtlar uygulama yeniden açılınca geri gelmez.
            if (db.Categories.Any() || db.Projects.Any()) return;

            var web = GetOrAddCategory(db, "Web Uygulamaları");
            var ai = GetOrAddCategory(db, "Yapay Zeka");
            var desktop = GetOrAddCategory(db, "Masaüstü");
            db.SaveChanges();

            AddProject(db, "Portfolyo Yönetim Sistemi", web.CategoryId, 25,
                "ASP.NET Core MVC ile geliştirilen, tüm içeriği admin panelinden yönetilen dinamik portfolyo sitesi.",
                "ASP.NET Core, EF Core, SQL Server, Bootstrap");
            AddProject(db, "E-Ticaret Sitesi", web.CategoryId, 18,
                "Ürün, kategori ve sepet yönetimi içeren katmanlı mimariyle hazırlanmış e-ticaret uygulaması.",
                "ASP.NET Core, EF Core, SQL Server, JavaScript");
            AddProject(db, "Akıllı Asistan", ai.CategoryId, 11,
                "Yapay zeka servisleri ile kullanıcı sorularını yanıtlayan sohbet tabanlı uygulama.",
                "C#, Web API, OpenAI, SQL Server");
            AddProject(db, "Stok Takip Programı", desktop.CategoryId, 5,
                "Ürün giriş-çıkışlarını ve stok durumunu takip eden masaüstü uygulaması.",
                "C#, Windows Forms, SQL Server");
            db.SaveChanges();
        }

        private static Category GetOrAddCategory(PortfolioContext db, string name)
        {
            var category = db.Categories.FirstOrDefault(x => x.CategoryName == name);
            if (category == null)
            {
                category = new Category { CategoryName = name };
                db.Categories.Add(category);
            }
            return category;
        }

        private static void AddProject(PortfolioContext db, string title, int categoryId, int daysAgo, string description, string technologies)
        {
            if (db.Projects.Any(x => x.ProjectTitle == title)) return;

            db.Projects.Add(new Project
            {
                ProjectTitle = title,
                ProjectDescription = description,
                Technologies = technologies,
                CategoryId = categoryId,
                IsActive = true,
                CreatedDate = DateTime.Now.AddDays(-daysAgo)
            });
        }
    }
}
