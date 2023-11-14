using InventoryApiAspCore.Interfaces.Auth;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryApiAspCore.Models.Expenses
{
    public class Expense : IUserEntity<object>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Amount { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Adding parents navigation FK
        public ExpenseType ExpenseType { get; set; }
    }
}
