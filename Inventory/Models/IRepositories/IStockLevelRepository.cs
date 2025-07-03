using Inventory.DTO_S;
using Inventory.DTO_S.Product;

namespace Inventory.Models.IRepositories
{
    public interface IStockLevelRepository
    {
        Task<IEnumerable<ProductLevelStockDTO>> GetStockLevelsAsync();

        Task<ProductLevelStockDTO> GetStockLevelsByIdAsync(int id);

        

        Task<IEnumerable<ProductLevelStockDTO>> GetLowStockProductsAsync();
    }
}
