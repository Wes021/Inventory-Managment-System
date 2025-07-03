namespace Inventory.Models
{
    public class LoginHistory
    {
        public int id { get; set; }

        public DateTime loginTime { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public virtual ApplicationUser User { get; set; } = null;
    }
}
