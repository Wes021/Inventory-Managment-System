using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.Models;


namespace Inventory.Mappers.StockTrsansAction
{
    public static class StockLevelMappers
    {
        public static Models.Product UpdateStockLevel(this LevelStockPatch levelStockPatch)
        {
            return new Models.Product
            {
                minimum_quantity_level=levelStockPatch.minimum_quantity_level
                
            };
        }

        public static Models.Product GetStockLevel(this ProductLevelStockDTO productLevelStockDTO)
        {
            return new Models.Product
            {
                ProductId = productLevelStockDTO.ProductId,
                ProductName = productLevelStockDTO.ProductName,
                Quantity= productLevelStockDTO.Quantity,
                minimum_quantity_level = productLevelStockDTO.minimum_quantity_level,
                Status = new ProductStatus
                {
                    ProductStatus1 = productLevelStockDTO.status
                }
            };
        }
    }
}
