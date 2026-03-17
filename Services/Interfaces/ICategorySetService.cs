using ProductValidation.DTOs.Category;
using ProductValidation.Models;

namespace ProductValidation.Services.Interfaces
{
    public interface ICategorySetService
    {
        public Task<Category> CreateCategoryService(CreateCategoryDto createCategoryDto);
        
    };
}