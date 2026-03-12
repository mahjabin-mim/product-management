using ProductValidation.Models;
using ProductValidation.DTOs.Category;
using ProductValidation.Repositories.Interfaces;

namespace ProductValidation.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<ReadCategoryDto> GetAllService()
        {
            var categoryList = categoryRepository.GetAll()
            .Select(c => new ReadCategoryDto
            {
                Name = c.Name
            });

            return categoryList;      
        } 
        
        public Category CreateCategoryService(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Id = new Random().Next(1, 10),
                Name = createCategoryDto.Name
            };

            categoryRepository.Create(category);
            return category;
        }

        public Category UpdateCategoryService(int id, CreateCategoryDto createCategoryDto)
        {
            var category = categoryRepository.GetById(id);
            if (category == null) return null;

            category.Name = createCategoryDto.Name;
            categoryRepository.Update(category);

            return category;
        }

        public bool DeleteCategoryService(int id)
        {
            return categoryRepository.Delete(id);
        }
    }
}