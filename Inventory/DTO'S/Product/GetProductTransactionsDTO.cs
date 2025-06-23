namespace Inventory.DTO_S.Product
{
    public class GetProductTransactionsDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public List<StockTransactionDTO> Transactions { get; set; }

        public class StockTransactionDTO
        {
            public int TransId { get; set; }
            public int Quantity { get; set; }
            public string transType { get; set; }
            public DateTime DataTime { get; set; }
            public string Note { get; set; } = null!;
        }
    }
}
