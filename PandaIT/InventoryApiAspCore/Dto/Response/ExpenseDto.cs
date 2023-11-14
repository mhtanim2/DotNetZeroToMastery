using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Response
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Amount { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }

        // Adding parents navigation FK
        public ExpenseTypeDto ExpenseType { get; set; }

    }
}
