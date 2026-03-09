using Microsoft.AspNetCore.Mvc;
using ProductValidation.DTOs.Category;
using ProductValidation.Models;
using ProductValidation.Services;

namespace ProductValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var categories = categoryService.GetAllService();
            return Ok(categories);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = categoryService.UpdateCategoryService(id, null);
            if (category == null)
                return NotFound();

            var readDto = new ReadCategoryDto
            {
                Name = category.Name,
            };

            return Ok(readDto);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateCategoryDto dto)
        {
            var category = categoryService.CreateCategoryService(dto);

            var readDto = new ReadCategoryDto
            {
                Name = category.Name,
            };

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, readDto);
        }

        // PUT: api/category/5
        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CreateCategoryDto dto)
        {
            var category = categoryService.UpdateCategoryService(id, dto);
            if (category == null)
                return NotFound();

            var readDto = new ReadCategoryDto
            {
                Name = category.Name,
            };

            return Ok(readDto);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = categoryService.DeleteCategoryService(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}