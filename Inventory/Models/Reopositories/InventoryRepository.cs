using AutoMapper;
using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.DTO_S.StockTransaction;
using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Models.Reopositories
{
    public class InventoryRepository : IinventoryRepository
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;
        private readonly IMapper _mapper;

        public InventoryRepository(InventoryManagmentSystemContext inventoryManagmentSystemContext, IMapper mapper)
        {
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
            _mapper = mapper;
        }

        private async Task<List<StockTransactionDTO>> GetStockTransactionsHelper(SqlCommand sc)
        {
            var stockTrans = new List<StockTransactionDTO>();

            using (SqlConnection conn = new SqlConnection(_inventoryManagmentSystemContext.Database.GetConnectionString()))
            {
                await conn.OpenAsync();

                sc.Connection = conn;


                using (SqlDataReader sdr = sc.ExecuteReader())
                {
                    while (await sdr.ReadAsync())
                    {
                        var stockTransaction = new StockTransactionDTO
                        {

                            TransId = sdr.GetInt32(sdr.GetOrdinal("trans_id")),
                            ProductId = sdr.GetInt32(sdr.GetOrdinal("product_id")),
                            Quantity = sdr.GetInt32(sdr.GetOrdinal("quantity")),
                            DataTime = sdr.GetDateTime(sdr.GetOrdinal("data_time")),
                            Note = sdr.GetString(sdr.GetOrdinal("note")),
                            user = sdr.GetString(sdr.GetOrdinal("fisrtName")) + " " + sdr.GetString(sdr.GetOrdinal("lastName")),
                            product = sdr.GetString(sdr.GetOrdinal("product_name")),
                            transType = sdr.GetString(sdr.GetOrdinal("type_name"))

                        };
                        stockTrans.Add(stockTransaction);

                    }

                }



            }

            return stockTrans;
        }

        public async Task<StockTransactionDTO> GetStockTransactionsByIdAsync(int id)
        {
            using (var sc = new SqlCommand("GetStockTransactionById"))
            {
                sc.CommandType = System.Data.CommandType.StoredProcedure;
                sc.Parameters.AddWithValue("@TransId", id);

                var stockTransactions = await GetStockTransactionsHelper(sc);
                return stockTransactions.FirstOrDefault();

            }
        }

        public async Task<List<StockTransactionDTO>> GetStockTransactionsAsync()
        {


            using (var sc = new SqlCommand("GetStockTransaction"))
            {
                return await GetStockTransactionsHelper(sc);
            }

        }



        public async Task<List<GetProductTransactionsDTO>> GetProductTransactionsAsync(int productId)
        {
            var flatTransactions = new List<FlatProductTransactionDTO>();

            using (SqlConnection conn = new SqlConnection(_inventoryManagmentSystemContext.Database.GetConnectionString()))
            {
                await conn.OpenAsync();

                using (SqlCommand sc = new SqlCommand("GetProductTransActions", conn))
                {
                    sc.CommandType = System.Data.CommandType.StoredProcedure;
                    sc.Parameters.AddWithValue("@ProductId", productId);

                    using (SqlDataReader sdr = await sc.ExecuteReaderAsync())
                    {
                        while (await sdr.ReadAsync())
                        {
                            flatTransactions.Add(new FlatProductTransactionDTO
                            {
                                ProductId = sdr.GetInt32(sdr.GetOrdinal("product_id")),
                                ProductName = sdr.GetString(sdr.GetOrdinal("product_name")),
                                TransId = sdr.GetInt32(sdr.GetOrdinal("trans_id")),
                                Quantity = sdr.GetInt32(sdr.GetOrdinal("quantity")),
                                transType = sdr.GetString(sdr.GetOrdinal("type_name")),
                                DataTime = sdr.GetDateTime(sdr.GetOrdinal("data_time")),
                                Note = sdr.IsDBNull(sdr.GetOrdinal("note")) ? null : sdr.GetString(sdr.GetOrdinal("note"))
                            });
                        }
                    }
                }
            }

            var result = flatTransactions
                .GroupBy(x => new { x.ProductId, x.ProductName })
                .Select(g => new GetProductTransactionsDTO
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    Transactions = g.Select(t => new GetProductTransactionsDTO.StockTransactionDTO
                    {
                        TransId = t.TransId,
                        Quantity = t.Quantity,
                        transType = t.transType,
                        DataTime = t.DataTime,
                        Note = t.Note
                    }).ToList()
                })
                .ToList();

            return result;
        }

        public async Task<int> NewTransactionAsync(StockTransaction transaction)
        {
            var product = await _inventoryManagmentSystemContext.Products.FindAsync(transaction.ProductId);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            
            if (product.Quantity < transaction.Quantity)
            {
                throw new InvalidOperationException("Insufficient stock.");
            }

            
            if ((product.Quantity - transaction.Quantity) < product.minimum_quantity_level)
            {
                throw new InvalidOperationException($"Transaction would drop stock below the minimum allowed level ({product.minimum_quantity_level}).");
            }

            
            product.Quantity -= transaction.Quantity;

            
            await _inventoryManagmentSystemContext.StockTransactions.AddAsync(transaction);

            
            _inventoryManagmentSystemContext.Products.Update(product);

            return await _inventoryManagmentSystemContext.SaveChangesAsync();
        }

    }
}
