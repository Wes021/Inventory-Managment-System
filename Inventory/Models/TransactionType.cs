using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class TransactionType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
