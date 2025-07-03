using Inventory.DTO_S.Product;
using Inventory.Models;

namespace Inventory.Mappers.Product
{
    public static class GetProductTransaciontMapper  
    {
        public static Models.Product ProductTransactionDTO(this GetProductTransactionsDTO getProductTransactionsDTO)
        {
            return new Models.Product
            {
                ProductName = getProductTransactionsDTO.ProductName,  
                Quantity = getProductTransactionsDTO.ProductId,   
            };
        }
    }
}
