using Microsoft.EntityFrameworkCore;
using ProductValidation.Data;
using ProductValidation.Models;
using ProductValidation.Repositories.Interfaces;

namespace ProductValidation.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Category Create(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return category;
        }
    }
}