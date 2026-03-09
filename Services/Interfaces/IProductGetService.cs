using ProductValidation.DTOs.Product;
using ProductValidation.Models;
using ProductValidation.Services.Interfaces;

namespace ProductValidation.Services.Interfaces
{
    public interface IProductGetService
    {
        IEnumerable<ReadProductDto> GetAllService();
        IEnumerable<ReadProductDto> GetProductInRangeService(decimal minPrice, decimal maxPrice);
    }
}