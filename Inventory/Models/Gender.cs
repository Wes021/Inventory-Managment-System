using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string Gender1 { get; set; } = null!;

    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}
