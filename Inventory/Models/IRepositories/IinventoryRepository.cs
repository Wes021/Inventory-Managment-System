using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.DTO_S.StockTransaction;

namespace Inventory.Models.IRepositories
{
    public interface IinventoryRepository
    {
        Task<IEnumerable<StockTransactionDTO>> GetStockTransactionsAsync();

        Task<StockTransactionDTO> GetStockTransactionsByIdAsync(int id);

        Task<IEnumerable<GetProductTransactionsDTO>> GetProductTransactionsAsync(int productId);

        //Task<int> NewTransactionAsync(StockTransaction transaction);

    }
}
