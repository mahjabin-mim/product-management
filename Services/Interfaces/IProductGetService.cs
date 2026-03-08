using ProductValidation.DTOs.Product;
using ProductValidation.Models;
using ProductValidation.Services.Interfaces;

namespace ProductValidation.Services.Interfaces
{
    public interface IProductGetService
    {
        IEnumerable<ReadProductDto> getAllService();
        IEnumerable<ReadProductDto> getProductInRangeService(decimal minPrice, decimal maxPrice);
    }
}