using ProductValidation.DTOs.Product;
using ProductValidation.Models;
using ProductValidation.Services.Interfaces;
using ProductValidation.Helpers;

namespace ProductValidation.Services.Interfaces
{
    public interface IProductGetService
    {
        public Task<IEnumerable<ReadProductDto>> GetAllService();
        public Task<IEnumerable<ReadProductDto>> GetProductInRangeService(decimal minPrice, decimal maxPrice);
        public Task<PageResponse<ReadProductDto>> GetProducts(QueryParams queryParams);

    }
}