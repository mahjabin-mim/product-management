using ProductValidation.Models;
using ProductValidation.DTOs.Category;
using ProductValidation.Repositories.Interfaces;
using ProductValidation.Services.Interfaces;

namespace ProductValidation.Services
{
    public class CategoryService : ICategorySetService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategoryService(CreateCategoryDto createCategoryDto)
        {
            var newCategory = new Category
            {
                Name = createCategoryDto.Name
            };

            await _categoryRepository.Create(newCategory);
            return newCategory;
        }
    }
}