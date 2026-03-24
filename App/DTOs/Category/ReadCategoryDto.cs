using System.ComponentModel.DataAnnotations;

namespace ProductValidation.DTOs.Category
{
    public class ReadCategoryDto
    {
        [Required]
        public required string Name { get; set; }
    }
}  