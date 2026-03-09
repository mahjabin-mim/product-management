using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string? Name { get; set; }
    }
}