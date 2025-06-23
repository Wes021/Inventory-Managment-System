using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class ProductStatus
{
    public int ProductStatusId { get; set; }

    public string ProductStatus1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
