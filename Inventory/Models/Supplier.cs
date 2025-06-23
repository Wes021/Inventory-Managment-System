using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
