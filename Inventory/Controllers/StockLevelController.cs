using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.Mappers.StockTrsansAction;
using Inventory.Models;
using Inventory.Models.IRepositories;
using Inventory.Models.Reopositories;
using Inventory.Services.IServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockLevelController : ControllerBase
    {
        private readonly IStockLevelRepository _stockLevelRepository;
        private readonly IProductRepository _productRepository;
        private readonly IinventoryServices _iinventoryServices;

        public StockLevelController(IStockLevelRepository stockLevelRepository, IProductRepository productRepository, IinventoryServices iinventoryServices)
        {
            _stockLevelRepository = stockLevelRepository;
            _productRepository = productRepository;
            _iinventoryServices = iinventoryServices;
        }

        
        [HttpGet("GetStockLevel")]
        public async Task<IActionResult> GetProducts()
        {
            var StoclLevels = await _stockLevelRepository.GetStockLevelsAsync();
            return Ok(StoclLevels);
        }


        
        [HttpGet("GetStockLevelById/{id}")]
        public async Task<IActionResult> GetStockLevelById(int id)
        {
            var StockLevel = await _stockLevelRepository.GetStockLevelsByIdAsync(id);
            if (StockLevel == null)
            {
                return NotFound($"Stock Level with Id = {id} not found");
            }
            return Ok(StockLevel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("UpdateMinMaxStockLevel/{id}")]
        public async Task<IActionResult> UpdateMinMaxStocklevel(LevelStockPatch levelStockPatch, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var productModel = levelStockPatch.UpdateStockLevel();
                await _iinventoryServices.UpdateMinMaxStockLevel(productModel, id);
                return Ok(productModel);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }


        


        
        [HttpGet("GetLowStockProducts")]
        public async Task<IActionResult> GetLowStockProducts()
        {
            var LowStoclLevels = await _stockLevelRepository.GetLowStockProductsAsync();
            return Ok(LowStoclLevels);
        }

    }
}
