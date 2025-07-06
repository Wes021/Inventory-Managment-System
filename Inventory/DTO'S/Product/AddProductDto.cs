namespace Inventory.DTO_S.Product
{
    public class AddProductDto
    {
        public string productName { get; set; }
        public int quantity { get; set; }
        public int categoryId { get; set; }
        public int supplierId { get; set; }
        public int status_id { get; set; }
        public string description { get; set; }
        public int minimum_quantity_level { get; set; }
        public int maximum_quantiry_level { get; set; }
    }
}
