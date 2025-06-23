using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class UserStatus
{
    public int UserStatusId { get; set; }

    public string UserStatus1 { get; set; }

    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

}
