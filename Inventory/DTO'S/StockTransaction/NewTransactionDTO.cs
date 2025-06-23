using Inventory.Models;

namespace Inventory.DTO_S.StockTransaction
{
    public class NewTransactionDTO
    {
        

        public int ProductId { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }

        public int TransTypeId { get; set; }

        public DateTime DataTime { get; set; }

        public string Note { get; set; } = null!;
    }
}
