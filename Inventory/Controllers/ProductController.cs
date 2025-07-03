using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.Mappers.Product;
using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProductAsync();
            return Ok(products);
        }


        
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var productModel = productDTO.ToProductFromCreateDTO();
                await _productRepository.AddProduct(productModel);
                return Ok(productModel);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }


        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with Id = {id} not found");
            }
            return Ok(product);
        }


        
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with Id = {id} not found");
            }
            try
            {
                await _productRepository.DeleteProduct(id);
                return Ok($"Product with Id = {id} has been deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdatePoductDTO updatePoductDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var productModel = updatePoductDTO.ToProductFromUpdateDTO();
                await _productRepository.UpdateProduct(productModel, id);
                return Ok(productModel);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
