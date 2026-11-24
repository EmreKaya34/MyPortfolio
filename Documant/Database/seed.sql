/* ============================================================
   Documant - örnek veri (SSMS'te çalıştırın)
   Veritabanı: PortFolioDayDb
   Önce Update-Database çalışmış olmalı (tablolar var olmalı).
   Script tekrar çalıştırılabilir: dolu tabloya/aynı kayda tekrar eklemez.
   ============================================================ */
USE [PortFolioDayDb];
GO
SET NOCOUNT ON;

/* ---------- Ana Sayfa (Hero) ---------- */
IF NOT EXISTS (SELECT 1 FROM heroSections)
    INSERT INTO heroSections (Greeting, Title, Description, MainImageUrl)
    VALUES (N'Merhaba, ben', N'Emre Kaya',
            N'ASP.NET Core ile modern, temiz ve ölçeklenebilir web uygulamaları geliştiriyorum. Öğrenmeyi ve ürettiklerimi paylaşmayı seviyorum.',
            N'/uploads/profile/profil.jpg');
ELSE
    UPDATE heroSections SET MainImageUrl = N'/uploads/profile/profil.jpg'
    WHERE MainImageUrl IS NULL OR MainImageUrl = N'';

/* ---------- Hakkımda ---------- */
IF NOT EXISTS (SELECT 1 FROM Abouts)
    INSERT INTO Abouts (FullName, JobTitle, Location, Description1, Description2, ImageUrl,
                        Stat1Value, Stat1Label, Stat2Value, Stat2Label, Stat3Value, Stat3Label)
    VALUES (N'Emre Kaya', N'Yazılım Geliştirici', N'İstanbul',
            N'Merhaba, ben Emre. .NET ekosisteminde web uygulamaları geliştiriyorum. ASP.NET Core MVC, Entity Framework Core ve SQL Server ile çalışmayı seviyorum.',
            N'Yazılımın yalnızca kod yazmak olmadığına, problem çözmek ve değer üretmek olduğuna inanıyorum. Yeni teknolojileri öğrenmeye ve projelerimde uygulamaya devam ediyorum.',
            N'/uploads/profile/profil.jpg',
            N'6+', N'Proje', N'10+', N'Teknoloji', N'100%', N'Öğrenme Tutkusu');

/* ---------- Yetenekler ---------- */
IF NOT EXISTS (SELECT 1 FROM skills)
    INSERT INTO skills (Title, Description, IconUrl, Level) VALUES
    (N'C# / ASP.NET Core',      N'MVC mimarisi, katmanlı yapı ve temiz kod ile web uygulamaları.', N'bi bi-server',     85),
    (N'Entity Framework Core',  N'Code First, migration, ilişkiler ve LINQ sorguları.',            N'bi bi-diagram-3',  80),
    (N'SQL Server',             N'İlişkisel veritabanı tasarımı, sorgular ve optimizasyon.',       N'bi bi-database',   75),
    (N'HTML / CSS / Bootstrap', N'Duyarlı, erişilebilir ve modern kullanıcı arayüzleri.',          N'bi bi-layout-wtf', 80),
    (N'JavaScript',             N'DOM işlemleri, AJAX ve etkileşimli arayüz bileşenleri.',         N'bi bi-braces',     65),
    (N'Git & GitHub',           N'Sürüm kontrolü, dal yönetimi ve ekip çalışması.',                N'bi bi-git',        70);

/* ---------- Deneyim (mevcut kayıtlar korunur; toplam en az 3 olur) ---------- */
IF (SELECT COUNT(*) FROM experiences) < 3
BEGIN
    IF NOT EXISTS (SELECT 1 FROM experiences WHERE Title = N'Yazılım Geliştirme Stajyeri')
        INSERT INTO experiences (Title, CompanyName, Description, CategoryName, IsActive)
        VALUES (N'Yazılım Geliştirme Stajyeri', N'Teknoloji Şirketi',
                N'ASP.NET Core ile kurumsal web uygulamalarının geliştirilmesine katkı verdim, veritabanı sorguları ve arayüz iyileştirmeleri üzerinde çalıştım.',
                N'Staj', 1);
    IF NOT EXISTS (SELECT 1 FROM experiences WHERE Title = N'.NET Eğitim Programı Katılımcısı')
        INSERT INTO experiences (Title, CompanyName, Description, CategoryName, IsActive)
        VALUES (N'.NET Eğitim Programı Katılımcısı', N'Yazılım Akademisi',
                N'C#, ASP.NET Core MVC, Entity Framework Core ve SQL Server konularında proje tabanlı yoğun bir eğitim programı tamamladım.',
                N'Eğitim', 1);
END

/* ---------- Eğitim ---------- */
IF NOT EXISTS (SELECT 1 FROM Educations)
    INSERT INTO Educations (Title, SchoolName, Description, Badge, DisplayOrder) VALUES
    (N'Yazılım Geliştirme Eğitimi — .NET', N'Yazılım Akademisi',
     N'ASP.NET Core MVC, Entity Framework Core, SQL Server ve modern web geliştirme üzerine proje tabanlı eğitim.', N'Devam Ediyor', 1),
    (N'Önlisans — Bilgisayar Programcılığı', N'Üniversite',
     N'Programlama temelleri, algoritmalar, veritabanı ve web teknolojileri üzerine temel eğitim.', N'Mezuniyet', 2);

/* ---------- İletişim ---------- */
IF NOT EXISTS (SELECT 1 FROM contacts)
    INSERT INTO contacts (Title, Description, Email, Location, PhoneNumber)
    VALUES (N'Harika Bir Fikrin mi Var? Birlikte Hayata Geçirelim.',
            N'Yeni projeler, iş birlikleri veya yalnızca teknoloji üzerine sohbet etmek için benimle iletişime geçebilirsin.',
            N'emre@example.com', N'İstanbul, Türkiye', N'+90 555 000 00 00');

/* ---------- Sosyal medya ---------- */
IF NOT EXISTS (SELECT 1 FROM SocialMedias)
    INSERT INTO SocialMedias (Name, Url, IconClass, IsActive) VALUES
    (N'GitHub',    N'https://github.com/EmreKaya34',                         N'bi bi-github',    1),
    (N'LinkedIn',  N'https://www.linkedin.com/in/emre-kaya-034612323/',      N'bi bi-linkedin',  1),
    (N'Instagram', N'https://www.instagram.com/eemrekaya034/',               N'bi bi-instagram', 1);

/* ---------- Kategoriler ---------- */
IF NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = N'Web Uygulamaları') INSERT INTO Categories (CategoryName) VALUES (N'Web Uygulamaları');
IF NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = N'Yapay Zeka')       INSERT INTO Categories (CategoryName) VALUES (N'Yapay Zeka');
IF NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = N'Masaüstü')         INSERT INTO Categories (CategoryName) VALUES (N'Masaüstü');

/* ---------- Projeler (Test / Tes2 gibi mevcut kayıtlara dokunulmaz) ---------- */
DECLARE @web int = (SELECT TOP 1 CategoryId FROM Categories WHERE CategoryName = N'Web Uygulamaları');
DECLARE @ai  int = (SELECT TOP 1 CategoryId FROM Categories WHERE CategoryName = N'Yapay Zeka');
DECLARE @dsk int = (SELECT TOP 1 CategoryId FROM Categories WHERE CategoryName = N'Masaüstü');

IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectTitle = N'Portfolyo Yönetim Sistemi')
    INSERT INTO Projects (ProjectTitle, ProjectDescription, ImageUrl, Technologies, IsActive, CreatedDate, CategoryId)
    VALUES (N'Portfolyo Yönetim Sistemi',
            N'ASP.NET Core MVC ile geliştirilen, tüm içeriği admin panelinden yönetilen dinamik portfolyo sitesi.',
            NULL, N'ASP.NET Core, EF Core, SQL Server, Bootstrap', 1, DATEADD(DAY, -25, GETDATE()), @web);

IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectTitle = N'E-Ticaret Sitesi')
    INSERT INTO Projects (ProjectTitle, ProjectDescription, ImageUrl, Technologies, IsActive, CreatedDate, CategoryId)
    VALUES (N'E-Ticaret Sitesi',
            N'Ürün, kategori ve sepet yönetimi içeren katmanlı mimariyle hazırlanmış e-ticaret uygulaması.',
            NULL, N'ASP.NET Core, EF Core, SQL Server, JavaScript', 1, DATEADD(DAY, -18, GETDATE()), @web);

IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectTitle = N'Akıllı Asistan')
    INSERT INTO Projects (ProjectTitle, ProjectDescription, ImageUrl, Technologies, IsActive, CreatedDate, CategoryId)
    VALUES (N'Akıllı Asistan',
            N'Yapay zeka servisleri ile kullanıcı sorularını yanıtlayan sohbet tabanlı uygulama.',
            NULL, N'C#, Web API, OpenAI, SQL Server', 1, DATEADD(DAY, -11, GETDATE()), @ai);

IF NOT EXISTS (SELECT 1 FROM Projects WHERE ProjectTitle = N'Stok Takip Programı')
    INSERT INTO Projects (ProjectTitle, ProjectDescription, ImageUrl, Technologies, IsActive, CreatedDate, CategoryId)
    VALUES (N'Stok Takip Programı',
            N'Ürün giriş-çıkışlarını ve stok durumunu takip eden masaüstü uygulaması.',
            NULL, N'C#, Windows Forms, SQL Server', 1, DATEADD(DAY, -5, GETDATE()), @dsk);

PRINT N'Örnek veri eklendi.';
GO
