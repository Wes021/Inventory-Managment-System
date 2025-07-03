using Inventory.DTO_S;
using Inventory.Mappers.Category;
using Inventory.Models;
using Inventory.Models.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetAll()
        {
            var categories = (await _categoryRepository.GetProductCategoryAsync()).ToList();
            return Ok(categories);
        }


        [HttpPost("AddCategory")]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = categoryDTO.ToCategoryFromCreateDTO();

            await _categoryRepository.AddCategory(categoryModel);

            return Ok(categoryModel);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("GetCategoryById/{Category_id}")]
        public async Task<IActionResult> GetById(int Category_id)
        {
            var category = (await _categoryRepository.GetProductCategoryByIdAsync(Category_id));
            return Ok(category);
        }

        [HttpGet("DeleteCategoryById/{Category_id}")]
        public async Task<IActionResult> DeleteById(int Category_id)
        {
            var category = await _categoryRepository.GetProductCategoryByIdAsync(Category_id);
            if (category == null)
            {
                return NotFound($"Category with Id = {Category_id} not found");
            }

            try
            {
                await _categoryRepository.DeleteCategory(Category_id);
                return Ok($"Category with Id = {Category_id} has been deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> Update(CategoryDTO categoryDTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var categoryModel = categoryDTO.ToCategoryFromUpdateDTO();
            try
            {
                await _categoryRepository.UpdateCategory(categoryModel, id);
                return Ok(categoryModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
