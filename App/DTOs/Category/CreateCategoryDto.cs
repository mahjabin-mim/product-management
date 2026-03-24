using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public required string Name { get; set; }
    }
}