using ProductValidation.Models;

namespace ProductValidation.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Category Create(Category category);
    }
}