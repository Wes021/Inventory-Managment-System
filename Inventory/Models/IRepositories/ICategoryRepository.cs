using Inventory.DTO_S;

namespace Inventory.Models.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetProductCategoryAsync();

        Task<int> AddCategory(ProductCategory productCategory);

        Task<int> UpdateCategory(ProductCategory productCategory, int id);

        Task<ProductCategory> GetProductCategoryByIdAsync(int id);

        Task<int> DeleteCategory(int id);
    }
}
