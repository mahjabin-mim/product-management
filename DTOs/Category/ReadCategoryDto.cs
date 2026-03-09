using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Category
{
    public class ReadCategoryDto
    {
        [Required]
        public string? Name { get; set; }
    }
}