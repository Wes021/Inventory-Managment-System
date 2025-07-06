using Sieve.Attributes;

namespace Inventory.DTO_S
{
    public class StockTransactionDTO
    {
        public int TransId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }
        public string Note { get; set; } = null!;
        public string user { get; set; }
        public string product { get; set; }
        public string transType { get; set; }
    }
}
