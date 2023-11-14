using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Request
{
    public class ExpenseRequestDto
    {
        [Required]
        public string Amount { get; set; }
        [MaxLength(255)]
        public string Note { get; set; }
    }
}
