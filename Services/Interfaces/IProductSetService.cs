using ProductValidation.DTOs.Product;
using ProductValidation.Models;

namespace ProductValidation.Services.Interfaces
{
    public interface IProductSetService
    {
        Product CreateProductService(CreateProductDto dto);
        bool UpdateProductService(int id, UpdateProductDto dto);

        bool DeleteProductService(int id);
    }
}