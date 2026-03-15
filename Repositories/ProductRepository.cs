using ProductValidation.Data;
using ProductValidation.Models;
using Microsoft.EntityFrameworkCore;
using ProductValidation.Repositories.Interfaces;

namespace ProductValidation.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products
                .Include(p => p.Category) // eager loading
                .AsNoTracking()
                .ToList();
        }

        public Product? GetById(int id)
        {
            return _dbContext.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Product> GetProductsInRange(decimal minPrice, decimal maxPrice)
        {
            return _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }

        public IQueryable<Product> GetProducts()
        {
            return _dbContext.Products.AsQueryable();
        }

    }
}