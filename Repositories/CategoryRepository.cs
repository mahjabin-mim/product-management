using Microsoft.EntityFrameworkCore;
using ProductValidation.Data;
using ProductValidation.Models;
using ProductValidation.Repositories.Interfaces;

namespace ProductValidation.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Category Create(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return category;
        }
    }
}