using Microsoft.EntityFrameworkCore;
using ProductValidation.Data;
using ProductValidation.Models;
using ProductValidation.Repositories;

namespace ProductValidation.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Category> GetAll()
        {
            return dbContext.Categories
                .AsNoTracking()
                .ToList();
        }

        public Category? GetById(int id)
        {
            return dbContext.Categories
                .FirstOrDefault(c => c.Id == id);
        }

        public Category Create(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return category;
        }

        public Category Update(Category category)
        {
            dbContext.Categories.Update(category);
            dbContext.SaveChanges();
            return category;
        }

        public bool Delete(int id)
        {
            var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return false;

            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
            return true;
        }

        public Category? GetWithProducts(int id)
        {
            return dbContext.Categories
                .Include(c => c.Products) 
                .FirstOrDefault(c => c.Id == id);
        }
    }
}