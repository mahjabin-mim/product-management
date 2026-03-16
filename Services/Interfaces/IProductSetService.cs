using ProductValidation.DTOs.Product;
using ProductValidation.Models;

namespace ProductValidation.Services.Interfaces
{
    public interface IProductSetService
    {
        public Task<Product> CreateProductService(CreateProductDto createProductDto);
        public Task<Product?> UpdateProductService(int id, UpdateProductDto updateProductDto);

        public Task<bool> DeleteProductService(int id);
    }
}