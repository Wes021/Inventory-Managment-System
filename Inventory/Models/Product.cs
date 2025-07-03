using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public int StatusId { get; set; }

    public string description { get; set; }

    public int minimum_quantity_level { get; set; }

    public int maximum_quantity_level { get; set; }

    public virtual ProductCategory Category { get; set; } = null!;

    public virtual ProductStatus Status { get; set; } = null!;

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();

    public virtual Supplier Supplier { get; set; } = null!;
}
