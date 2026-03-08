using ProductValidation.DTOs.Product;
using ProductValidation.Models;

namespace ProductValidation.Services.Interfaces
{
    public interface IProductSetService
    {
        Product CreateProductService(CreateProductDto createProductDto);
        Product UpdateProductService(int id, UpdateProductDto updateProductDto);

        bool DeleteProductService(int id);
    }
}