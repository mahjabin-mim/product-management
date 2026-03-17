using Microsoft.AspNetCore.Mvc;
using ProductValidation.Services.Interfaces;
using ProductValidation.DTOs.Category;

namespace ProductValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategorySetService _setService;
        public CategoryController(ICategorySetService setService)
        {
            _setService = setService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = await _setService.CreateCategoryService(createCategoryDto);
            return Ok(category);
        }
  
    }
}