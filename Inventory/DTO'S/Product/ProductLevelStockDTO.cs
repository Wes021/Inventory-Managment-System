namespace Inventory.DTO_S.Product
{
    public class ProductLevelStockDTO
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; } = null!;

        public int minimum_quantity_level { get; set; }

        public string status { get; set; }
    }
}
