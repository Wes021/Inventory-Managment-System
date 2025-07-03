using Inventory.DTO_S;
using Inventory.DTO_S.StockTransaction;
using Inventory.Mappers.StockTrsansAction;
using Inventory.Models.IRepositories;
using Inventory.Services.IServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IinventoryRepository _inventoryRepository;
        private readonly IinventoryServices _iinventoryServices;
        public InventoryController(IinventoryRepository inventoryRepository, IinventoryServices iinventoryServices)
        {
            _inventoryRepository = inventoryRepository;
            _iinventoryServices = iinventoryServices;
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


        
        [HttpGet("GetProductTransactionByProductId/{productId}")]
        public async Task<IActionResult> GetTransactionsByProduct(int productId)
        {
            var result = await _inventoryRepository.GetProductTransactionsAsync(productId);
            return Ok(result);
        }


        [Authorize(Roles = "Employee")]
        [HttpPost("MakeNewOutTransaction")]
        public async Task<IActionResult> MakeNewOutTransaction(NewTransactionDTO newTransactionDTO)
        {
            var transaction = newTransactionDTO.ToStockTransaction(); 
            await _iinventoryServices.NewOutTransactionAsync(transaction); 
            return Ok();
        }


        [Authorize(Roles = "Employee")]
        [HttpPost("MakeNewInTransaction")]
        public async Task<IActionResult> MakeNewInTransaction(NewTransactionDTO newTransactionDTO)
        {
            var transaction = newTransactionDTO.ToStockTransaction(); 
            await _iinventoryServices.NewInTransactionAsync(transaction); 
            return Ok();
        }

    }
}