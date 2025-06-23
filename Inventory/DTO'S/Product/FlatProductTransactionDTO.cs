namespace Inventory.DTO_S.Product
{
    public class FlatProductTransactionDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int TransId { get; set; }
        public int Quantity { get; set; }
        public string transType { get; set; } = null!;
        public DateTime DataTime { get; set; }
        public string Note { get; set; } = null!;
    }
}
