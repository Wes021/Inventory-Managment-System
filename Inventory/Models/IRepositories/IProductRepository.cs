﻿using Inventory.DTO_S;
using Inventory.DTO_S.Product;

namespace Inventory.Models.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductsDTO>> GetProductAsync();

        Task<int> AddProduct(AddProductDto addProductDto);

        Task<int> UpdateProduct(Product product, int id);

        Task<GetProductsDTO> GetProductByIdAsync(int id);

        Task<int> DeleteProduct(int id);


    }
}
