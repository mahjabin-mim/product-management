using ProductValidation.Data;
using ProductValidation.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductValidation.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return dbContext.Products
                .Include(p => p.Category) // eager loading
                .AsNoTracking()
                .ToList();
        }

        public Product? GetById(int id)
        {
            return dbContext.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            dbContext.Products.Update(product);
            dbContext.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return false;

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Product> GetProductsInRange(decimal minPrice, decimal maxPrice)
        {
            return dbContext.Products
                .AsNoTracking()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }
    }
}