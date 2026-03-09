using ProductValidation.Models;

namespace ProductValidation.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        Product Create(Product product);

        Product Update(Product product);

        bool Delete(int id);

        IEnumerable<Product> GetProductsInRange(decimal minPrice, decimal maxPrice);
    }
}