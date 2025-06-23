using Inventory.Models.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Models.Reopositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryManagmentSystemContext _inventoryManagmentSystemContext;

        public CategoryRepository(InventoryManagmentSystemContext inventoryManagmentSystemContext)
        {
            _inventoryManagmentSystemContext = inventoryManagmentSystemContext;
        }


        public async Task<int> AddCategory(ProductCategory productCategory)
        {
            bool checkCatogory = await _inventoryManagmentSystemContext.ProductCategories.AnyAsync(c => c.ProductCategoryName == productCategory.ProductCategoryName);

            if (checkCatogory)
            {
                throw new Exception("Category Already exists");
            }

            _inventoryManagmentSystemContext.ProductCategories.Add(productCategory);
            return await _inventoryManagmentSystemContext.SaveChangesAsync();
        }







        public async Task<IEnumerable<ProductCategory>> GetProductCategoryAsync()
        {
            return await _inventoryManagmentSystemContext.ProductCategories.OrderBy(c => c.ProductCategoryId).ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            return await _inventoryManagmentSystemContext.ProductCategories.FirstOrDefaultAsync(c => c.ProductCategoryId == id);
            throw new NotImplementedException();
        }





        public async Task<int> UpdateCategory(ProductCategory productCategory, int id)
        {
            var categoryToUpdate = await _inventoryManagmentSystemContext.ProductCategories
                .FirstOrDefaultAsync(c => c.ProductCategoryId == id);
            if (categoryToUpdate == null)
            {
                throw new Exception("Category does not exist");
            }

            categoryToUpdate.ProductCategoryName = productCategory.ProductCategoryName;

            _inventoryManagmentSystemContext.ProductCategories.Update(categoryToUpdate);
            return await _inventoryManagmentSystemContext.SaveChangesAsync();
        }


        public async Task<int> DeleteCategory(int id)
        {
            bool checkCategoryExist = await _inventoryManagmentSystemContext.ProductCategories.AnyAsync(c => c.ProductCategoryId == id);

            bool AnyProducts = await _inventoryManagmentSystemContext.Products.AnyAsync(c => c.CategoryId == id);

            if (checkCategoryExist & AnyProducts)
            {
                throw new Exception("Category does not exist or attached to products");
            }

            var categoryTodelete = await _inventoryManagmentSystemContext.ProductCategories.FirstOrDefaultAsync(c => c.ProductCategoryId == id);

            _inventoryManagmentSystemContext.ProductCategories.Remove(categoryTodelete);
            return await _inventoryManagmentSystemContext.SaveChangesAsync();
        }
    }
}
