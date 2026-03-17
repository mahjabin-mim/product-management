using ProductValidation.DTOs.Product;
using ProductValidation.Models;

namespace ProductValidation.Services.Interfaces
{
    public interface IProductSetService
    {
        public Task<ReadProductDto> CreateProductService(CreateProductDto createProductDto);
        public Task<ReadProductDto?> UpdateProductService(int id, UpdateProductDto updateProductDto);

        public Task<bool> DeleteProductService(int id);
    }
}