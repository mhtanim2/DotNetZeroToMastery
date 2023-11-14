using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Request
{
    public class ExpenseTypeRequestDto
    {
        [Required]
        
        public string Name { get; set; }

    }
}
