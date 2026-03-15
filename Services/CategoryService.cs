using ProductValidation.Models;
using ProductValidation.DTOs.Category;
using ProductValidation.Repositories.Interfaces;

namespace ProductValidation.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateCategoryService(CreateCategoryDto createCategoryDto)
        {
            var newCategory = new Category
            {
                Name = createCategoryDto.Name
            };

            return _categoryRepository.Create(newCategory);
        }
    }
}