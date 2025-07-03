using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.Models.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Models.Reopositories
{
    public class StockLevelRepository : IStockLevelRepository
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;

        public StockLevelRepository(InventoryManagmentSystemContext inventoryManagmentSystemContext)
        {
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
        }

        private async Task<IEnumerable<ProductLevelStockDTO>> GetStockLevelHepler(SqlCommand sc)
        {
            var StockLevels = new List<ProductLevelStockDTO>();

            using (SqlConnection conn = new SqlConnection(_inventoryManagmentSystemContext.Database.GetConnectionString()))
            {
                await conn.OpenAsync();
                sc.Connection = conn;
                using (SqlDataReader sdr = sc.ExecuteReader())
                {
                    while (await sdr.ReadAsync())
                    {
                        var stockLevels = new ProductLevelStockDTO
                        {
                            ProductId = sdr.GetInt32(sdr.GetOrdinal("product_id")),
                            Quantity = sdr.GetInt32(sdr.GetOrdinal("quantity")),
                            ProductName = sdr.GetString(sdr.GetOrdinal("product_name")),
                            status = sdr.GetString(sdr.GetOrdinal("product_status")),
                            minimum_quantity_level = sdr.GetInt32(sdr.GetOrdinal("minimum_quantity_level")),
                            maximum_quantity_level=sdr.GetInt32(sdr.GetOrdinal("maximum_quantity_level"))
                            

                        };

                        StockLevels.Add(stockLevels);
                    }
                }

            }

            return StockLevels;
        }

        public async Task<IEnumerable<ProductLevelStockDTO>> GetStockLevelsAsync()
        {


            using (SqlCommand sc = new SqlCommand("GetStockLevels"))
            {
                return await GetStockLevelHepler(sc);
            }



        }

        public async Task<ProductLevelStockDTO> GetStockLevelsByIdAsync(int id)
        {
            using (SqlCommand sc = new SqlCommand("GetStockLevelById"))
            {
                sc.CommandType = System.Data.CommandType.StoredProcedure;
                sc.Parameters.AddWithValue("@product_id", id);
                var result = await GetStockLevelHepler(sc);
                return result.FirstOrDefault();

            }
            throw new NotImplementedException();
        }





        public async Task<IEnumerable<ProductLevelStockDTO>> GetLowStockProductsAsync()
        {

            using (SqlCommand sc = new SqlCommand("GetLowStockProducts"))
            {
                return await GetStockLevelHepler(sc);
            }
        }
    }
}
