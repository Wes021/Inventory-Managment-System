using Microsoft.AspNetCore.Identity;

namespace Inventory.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string fisrtName { get; set; }
        public string lastName { get; set; }

        public int GenderId { get; set; }


        public int UserStatusId { get; set; }

        public virtual Gender gender { get; set; } = null!;

        public virtual UserStatus Status { get; set; } = null!;


    }
}
