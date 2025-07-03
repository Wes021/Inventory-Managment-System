using Inventory.Models;

namespace Inventory.Services.IServicesInterfaces
{
    public interface IinventoryServices
    {
        Task<int> NewOutTransactionAsync(StockTransaction transaction);

        Task<int> NewInTransactionAsync(StockTransaction transaction);

        Task<int> UpdateMinMaxStockLevel(Product product, int id);

       
    }
}
