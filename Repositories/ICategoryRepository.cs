using ProductValidation.Models;

namespace ProductValidation.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();

        Category GetById(int id);

        Category Create(Category category);

        Category Update(Category category);

        bool Delete(int id);
        
        Category GetWithProducts(int id);

    }
}