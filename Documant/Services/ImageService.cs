using Documant.Context;

namespace Documant.Services
{
    public record ImageUploadResult(bool Success, string? Path, string? Error);

    public interface IImageService
    {
        Task<ImageUploadResult> SaveAsync(IFormFile file, string folder);
        void Delete(string? relativePath);
    }

    /// <summary>
    /// IFormFile ile gelen görselleri doğrular (tür, boyut, dosya imzası),
    /// güvenli (GUID) isimle wwwroot/uploads altına kaydeder. DB'de sadece yol tutulur.
    /// </summary>
    public class ImageService : IImageService
    {
        private const long MaxFileSizeBytes = 2 * 1024 * 1024; // 2 MB
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        private readonly IWebHostEnvironment _env;
        private readonly PortfolioContext _context;

        public ImageService(IWebHostEnvironment env, PortfolioContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<ImageUploadResult> SaveAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                return new ImageUploadResult(false, null, "Lütfen bir görsel dosyası seçiniz.");

            if (file.Length > MaxFileSizeBytes)
                return new ImageUploadResult(false, null, "Görsel boyutu en fazla 2 MB olabilir.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension) || !file.ContentType.StartsWith("image/"))
                return new ImageUploadResult(false, null, "Yalnızca JPG, PNG veya WEBP görsel yükleyebilirsiniz.");

            if (!await HasValidSignatureAsync(file))
                return new ImageUploadResult(false, null, "Dosya içeriği geçerli bir görsel değil.");

            var safeFileName = $"{Guid.NewGuid():N}{extension}";
            var directory = Path.Combine(_env.WebRootPath, "uploads", folder);
            Directory.CreateDirectory(directory);

            await using var stream = new FileStream(Path.Combine(directory, safeFileName), FileMode.Create);
            await file.CopyToAsync(stream);

            return new ImageUploadResult(true, $"/uploads/{folder}/{safeFileName}", null);
        }

        public void Delete(string? relativePath)
        {
            // Sadece kendi yüklediğimiz dosyalar silinir (harici URL'lere dokunulmaz)
            if (string.IsNullOrWhiteSpace(relativePath) || !relativePath.StartsWith("/uploads/"))
                return;

            // Aynı fotoğrafı başka bir kayıt da kullanıyorsa (örn. Ana Sayfa + Hakkımda) dosya silinmez
            if (IsInUse(relativePath))
                return;

            var uploadsRoot = Path.GetFullPath(Path.Combine(_env.WebRootPath, "uploads"));
            var fullPath = Path.GetFullPath(Path.Combine(_env.WebRootPath, relativePath.TrimStart('/')));

            if (fullPath.StartsWith(uploadsRoot) && File.Exists(fullPath))
                File.Delete(fullPath);
        }

        private bool IsInUse(string path) =>
            _context.heroSections.Any(x => x.MainImageUrl == path)
            || _context.Abouts.Any(x => x.ImageUrl == path)
            || _context.Projects.Any(x => x.ImageUrl == path);

        // Uzantı sahte olabilir; dosyanın ilk baytlarına bakarak gerçek türü doğrularız
        private static async Task<bool> HasValidSignatureAsync(IFormFile file)
        {
            var header = new byte[12];
            await using var stream = file.OpenReadStream();
            var read = await stream.ReadAsync(header, 0, header.Length);
            if (read < 12) return false;

            var isJpeg = header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;
            var isPng = header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
            var isWebp = header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46
                         && header[8] == 0x57 && header[9] == 0x45 && header[10] == 0x42 && header[11] == 0x50;

            return isJpeg || isPng || isWebp;
        }
    }
}
