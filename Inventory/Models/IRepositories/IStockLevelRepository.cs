using Inventory.DTO_S;
using Inventory.DTO_S.Product;

namespace Inventory.Models.IRepositories
{
    public interface IStockLevelRepository
    {
        Task<List<ProductLevelStockDTO>> GetStockLevelsAsync();

        Task<ProductLevelStockDTO> GetStockLevelsByIdAsync(int id);

        Task<int> UpdateMinimunStockLevel(Product product, int id);

        Task<List<ProductLevelStockDTO>> GetLowStockProductsAsync();
    }
}
