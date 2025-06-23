using Inventory.DTO_S;
using Inventory.Models;

namespace Inventory.Mappers.Category
{
    public static class CategoryMappers
    {
        public static ProductCategory ToCategoryFromCreateDTO(this CategoryDTO createUpdateCategory)
        {
            return new ProductCategory
            {
                ProductCategoryName=createUpdateCategory.ProductCategoryName
            };
        }

        public static ProductCategory ToCategoryFromUpdateDTO(this CategoryDTO createUpdateCategory)
        {
            return new ProductCategory
            {
                ProductCategoryName = createUpdateCategory.ProductCategoryName
            };
        }
    }
}
