using Inventory.DTO_S;
using Inventory.Models;

namespace Inventory.Mappers.StockTrsansAction
{
    public static class StockTransactionMappers
    {
        public static StockTransaction ToStockTransactionsFromReadDTO(this StockTransactionDTO stockTransactionDTOproductDTO)
        {
            return new StockTransaction
            {
                TransId = stockTransactionDTOproductDTO.TransId,
                ProductId = stockTransactionDTOproductDTO.ProductId,
                Quantity = stockTransactionDTOproductDTO.Quantity,
                DataTime = stockTransactionDTOproductDTO.TransactionDate,
                Note = stockTransactionDTOproductDTO.Note,
                User = new ApplicationUser
                {
                    UserName = stockTransactionDTOproductDTO.user
                },
                Product = new Models.Product
                {
                    ProductName = stockTransactionDTOproductDTO.product
                },
                TransType = new TransactionType
                {
                    TypeName = stockTransactionDTOproductDTO.transType
                }
                };



        }
    }
}
