using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class StockTransaction
{
    public int TransId { get; set; }

    public int ProductId { get; set; }

    public string UserId { get; set; }

    public int Quantity { get; set; }

    public int TransTypeId { get; set; }

    public DateTime DataTime { get; set; }

    public string Note { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null;

    public virtual Product Product { get; set; } = null!;

    public virtual TransactionType TransType { get; set; } = null!;
}
