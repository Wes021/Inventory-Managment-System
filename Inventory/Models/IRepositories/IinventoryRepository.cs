using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.DTO_S.StockTransaction;

namespace Inventory.Models.IRepositories
{
    public interface IinventoryRepository
    {
        Task<List<StockTransactionDTO>> GetStockTransactionsAsync();

        Task<StockTransactionDTO> GetStockTransactionsByIdAsync(int id);

        Task<List<GetProductTransactionsDTO>> GetProductTransactionsAsync(int productId);

        Task<int> NewTransactionAsync(StockTransaction transaction);

    }
}
