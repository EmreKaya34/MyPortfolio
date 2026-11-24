using Documant.Entities;

namespace Documant.Models
{
    public class HeroViewModel
    {
        public string Greeting { get; set; } = "Merhaba, ben";
        public string Title { get; set; } = "Portfolyo";
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public List<string> RotatingTitles { get; set; } = new();
        public string Stat1Value { get; set; } = string.Empty;
        public string Stat1Label { get; set; } = string.Empty;
        public string Stat3Value { get; set; } = string.Empty;
        public string Stat3Label { get; set; } = string.Empty;
        public List<SocialMedia> Socials { get; set; } = new();
    }

    public class ContactSectionViewModel
    {
        public Contact? Contact { get; set; }
        public List<SocialMedia> Socials { get; set; } = new();
    }

    public class FooterViewModel
    {
        public string OwnerName { get; set; } = "Portfolyo";
        public List<SocialMedia> Socials { get; set; } = new();
    }
}
