using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Request
{
    public class CategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
