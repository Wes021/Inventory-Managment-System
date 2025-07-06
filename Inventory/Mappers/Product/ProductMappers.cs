using Inventory.DTO_S;
using Inventory.DTO_S.Product;
using Inventory.Models;

namespace Inventory.Mappers.Product
{
    public static class ProductMappers
    {
        public static Models.Product ToProductFromCreateDTO(this ProductDTO productDTO)
        {
            return new Models.Product
            {
                ProductName = productDTO.ProductName,
                Quantity = productDTO.Quantity,
                CategoryId = productDTO.CategoryId,
                SupplierId = productDTO.SupplierId,
                StatusId = productDTO.StatusId,
                description = productDTO.Description,
            };
        }

        public static Models.Product ToProductFromReadDTO(this GetProductsDTO getProductsDTO)
        {
            return new Models.Product
            {
                ProductId=getProductsDTO.ProductId,
                ProductName = getProductsDTO.ProductName,
                Quantity = getProductsDTO.Quantity,
                minimum_quantity_level=getProductsDTO.minimum_quantity_level,
                maximum_quantity_level=getProductsDTO.maximum_quantity_level,
                Category = new ProductCategory
                {
                    ProductCategoryName = getProductsDTO.Category
                },
                Status = new ProductStatus
                {
                    ProductStatus1 = getProductsDTO.status
                },
                Supplier = new Supplier
                {
                    SupplierName = getProductsDTO.supplier
                },
                description = getProductsDTO.Description,
            };
        }


        public static Models.Product ToProductFromUpdateDTO(this UpdatePoductDTO updatePoductDTO)
        {
            return new Models.Product
            {
                ProductName = updatePoductDTO.ProductName,
                Quantity = updatePoductDTO.Quantity,
                CategoryId = updatePoductDTO.CategoryId,
                SupplierId = updatePoductDTO.SupplierId,
                StatusId = updatePoductDTO.StatusId,
                description = updatePoductDTO.Description,
            };
        }



        public static Models.Product AddProductMapper(this AddProductDto addProductDto)
        {
            return new Models.Product
            {
                ProductName=addProductDto.productName,
                Quantity=addProductDto.quantity,
                CategoryId=addProductDto.categoryId,
                SupplierId=addProductDto.supplierId,
                StatusId=addProductDto.status_id,
                description=addProductDto.description,
                minimum_quantity_level=addProductDto.minimum_quantity_level,
                maximum_quantity_level=addProductDto.maximum_quantiry_level
            };
        }
    }
}
