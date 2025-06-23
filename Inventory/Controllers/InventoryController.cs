using Inventory.DTO_S;
using Inventory.DTO_S.StockTransaction;
using Inventory.Mappers.StockTrsansAction;
using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IinventoryRepository _inventoryRepository;
        public InventoryController(IinventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet("GetStocktransaction")]
        public async Task<IActionResult> GetStockTrans()
        {
            var stockTransaction = await _inventoryRepository.GetStockTransactionsAsync();

            return Ok(stockTransaction);
        }

        [HttpGet("GetStockTransactionById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _inventoryRepository.GetStockTransactionsByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetTransactionsByProduct(int productId)
        {
            var result = await _inventoryRepository.GetProductTransactionsAsync(productId);
            return Ok(result);
        }


        [HttpPost("MakeNewTransaction")]
        public async Task<IActionResult> MakeNewTransaction(NewTransactionDTO newTransactionDTO)
        {
            var transaction = newTransactionDTO.ToStockTransaction(); // this returns a StockTransaction
            await _inventoryRepository.NewTransactionAsync(transaction); // now the type matches
            return Ok();
        }

    }
}