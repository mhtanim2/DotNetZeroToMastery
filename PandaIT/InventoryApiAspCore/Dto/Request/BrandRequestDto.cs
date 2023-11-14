using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Request
{
    public class BrandRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
