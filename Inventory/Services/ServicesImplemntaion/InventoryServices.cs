using Inventory.Models;
using Inventory.Services.IServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services.ServicesImplemntaion
{
    public class InventoryServices : IinventoryServices
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;

        public InventoryServices(InventoryManagmentSystemContext inventoryManagmentSystemContext)
        {
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
        }

        public async Task<int> NewInTransactionAsync(StockTransaction transaction)
        {

            var product = await _inventoryManagmentSystemContext.Products.FindAsync(transaction.ProductId);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }


            //if (product.Quantity < transaction.Quantity)
            //{
            //    throw new InvalidOperationException("Insufficient stock.");
            //}


            if ((product.Quantity + transaction.Quantity) > product.maximum_quantity_level)
            {
                throw new InvalidOperationException($"Transaction would exceed stock above the maximum allowed level ({product.maximum_quantity_level}).");
            }


            product.Quantity += transaction.Quantity;


            await _inventoryManagmentSystemContext.StockTransactions.AddAsync(transaction);


            _inventoryManagmentSystemContext.Products.Update(product);

            return await _inventoryManagmentSystemContext.SaveChangesAsync();
        }

        public async Task<int> NewOutTransactionAsync(StockTransaction transaction)
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

        //public async Task<int> UpdateMaximumStockLevel(Product product, int id)
        //{
        //    var existingProduct = await _inventoryManagmentSystemContext.Products
        //       .FirstOrDefaultAsync(s => s.ProductId == id);
        //    if (existingProduct == null)
        //    {
        //        throw new InvalidOperationException($"Product with Id = {id} not found.");
        //    }
        //    existingProduct.maximum_quantity_level = product.maximum_quantity_level;

            
        //    return await _inventoryManagmentSystemContext.SaveChangesAsync();
        //}

        public async Task<int> UpdateMinMaxStockLevel(Product product, int id)
        {
            var existingProduct = await _inventoryManagmentSystemContext.Products
               .FirstOrDefaultAsync(s => s.ProductId == id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException($"Product with Id = {id} not found.");
            }
            existingProduct.minimum_quantity_level = product.minimum_quantity_level;
            existingProduct.maximum_quantity_level = product.maximum_quantity_level;

           
            return await _inventoryManagmentSystemContext.SaveChangesAsync();
            
        }
    }
}
