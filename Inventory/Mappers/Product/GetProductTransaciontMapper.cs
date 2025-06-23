using Inventory.DTO_S.Product;
using Inventory.Models;

namespace Inventory.Mappers.Product
{
    public static class GetProductTransaciontMapper // Changed to static class to fix CS1106  
    {
        public static Models.Product ProductTransactionDTO(this GetProductTransactionsDTO getProductTransactionsDTO)
        {
            return new Models.Product
            {
                ProductName = getProductTransactionsDTO.ProductName, // Fixed incorrect variable name  
                Quantity = getProductTransactionsDTO.ProductId, // Fixed incorrect variable name  
            };
        }
    }
}
