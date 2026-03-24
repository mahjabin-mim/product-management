using ProductValidation.Models;

namespace ProductValidation.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> Create(Category category);
        public Task<Category?> GetByName(string name);
    }
}