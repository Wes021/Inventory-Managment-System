using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.Models.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace Inventory.Models.Reopositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;

        public ProductRepository(InventoryManagmentSystemContext inventoryManagmentSystemContext)
        {
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
        }

        private async Task<IEnumerable<GetProductsDTO>> GetProductsHelpers(SqlCommand sc)
        {
            var products = new List<GetProductsDTO>();
            using (SqlConnection conn = new SqlConnection(_inventoryManagmentSystemContext.Database.GetConnectionString()))
            {
                await conn.OpenAsync();

                sc.Connection = conn;


                using (SqlDataReader sdr = sc.ExecuteReader())
                {
                    while (await sdr.ReadAsync())
                    {
                        var product = new GetProductsDTO
                        {
                            ProductId = sdr.GetInt32(sdr.GetOrdinal("product_id")),
                            Quantity = sdr.GetInt32(sdr.GetOrdinal("quantity")),
                            ProductName = sdr.GetString(sdr.GetOrdinal("product_name")),
                            Description = sdr.GetString(sdr.GetOrdinal("description")),
                            supplier = sdr.GetString(sdr.GetOrdinal("supplier")),
                            status = sdr.GetString(sdr.GetOrdinal("status")),
                            Category = sdr.GetString(sdr.GetOrdinal("category")),
                            minimum_quantity_level = sdr.GetInt32(sdr.GetOrdinal("minimum_quantity_level")),
                            maximum_quantity_level=sdr.GetInt32(sdr.GetOrdinal("maximum_quantity_level"))

                        };
                        products.Add(product);
                    }
                }



            }

            return products;
        }

        public async Task<IEnumerable<GetProductsDTO>> GetProductAsync()
        {

            using (SqlCommand sc = new SqlCommand("GetProducts"))
            {

                return await GetProductsHelpers(sc);

            }


        }


        public async Task<GetProductsDTO> GetProductByIdAsync(int id)
        {
            using (SqlCommand sc = new SqlCommand("GetProducts"))
            {

                sc.Parameters.AddWithValue("@ProductId", id);
                var result = await GetProductsHelpers(sc);
                return result.FirstOrDefault();

            }

            throw new NotImplementedException();
        }


        public Task<int> AddProduct(Product product)
        {
            var existingProduct = _inventoryManagmentSystemContext.Products
                .FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                throw new InvalidOperationException("Product with the same ID already exists.");
            }
            _inventoryManagmentSystemContext.Products.Add(product);
            return _inventoryManagmentSystemContext.SaveChangesAsync();



        }


        public Task<int> UpdateProduct(Product product, int id)
        {
            var existingProduct = _inventoryManagmentSystemContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException($"Product with Id = {product.ProductId} not found.");
            }
            _inventoryManagmentSystemContext.Products.Update(product);
            return _inventoryManagmentSystemContext.SaveChangesAsync();


            throw new NotImplementedException();
        }



        public async Task<int> DeleteProduct(int id)
        {
            var product = await _inventoryManagmentSystemContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Id = {id} not found.");
            }
            _inventoryManagmentSystemContext.Products.Remove(product);
            return await _inventoryManagmentSystemContext.SaveChangesAsync();

            throw new NotImplementedException();
        }
    }
}
