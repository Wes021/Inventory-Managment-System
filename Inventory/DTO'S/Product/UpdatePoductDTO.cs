namespace Inventory.DTO_S.Product
{
    public class UpdatePoductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }

        public int StatusId { get; set; }

        public string Description { get; set; }

        public int minimum_quantity_level { get; set; }

        public int maximum_quantity_level { get; set; }

    }
}
