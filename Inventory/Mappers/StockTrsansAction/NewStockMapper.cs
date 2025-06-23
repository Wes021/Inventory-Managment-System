using AutoMapper;
using Inventory.DTO_S.StockTransaction;
using Inventory.DTO_S.Supplier;
using Inventory.Models;

namespace Inventory.Mappers.StockTrsansAction
{
    public static class NewStockMapper
    {
        public static StockTransaction ToStockTransaction(this NewTransactionDTO dto)
        {
            return new StockTransaction
            {
                
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Quantity = dto.Quantity,
                TransTypeId = dto.TransTypeId,
                DataTime = dto.DataTime,
                Note = dto.Note
            };
        }
    }

}