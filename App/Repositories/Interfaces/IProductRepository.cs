using ProductValidation.DTOs.Product;
using ProductValidation.Models;

namespace ProductValidation.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAll();

        public Task<Product?> GetById(int id);

        public Task<Product> Create(Product product);

        public Task<Product> Update(Product product);

        public Task<bool> Delete(int id);

        public Task<IEnumerable<Product>> GetProductsInRange(decimal minPrice, decimal maxPrice);
        IQueryable<ReadProductDto> GetProducts();

    }
}