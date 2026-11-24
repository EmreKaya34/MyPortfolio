namespace Documant.Areas.Admin.Models
{
    public class AdminSidebarViewModel
    {
        public string OwnerName { get; set; } = "Portfolyo";
        public int UnreadMessageCount { get; set; }
        public string CurrentController { get; set; } = string.Empty;
    }
}
