using ProductValidation.DTOs.Category;
using ProductValidation.Models;

namespace ProductValidation.Services.Interfaces
{
    public interface ICategorySetService
    {
        Category CreateCategoryService(CreateCategoryDto createCategoryDto);
        
    };
}