using InventoryApiAspCore.Interfaces.Auth;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Models.Expenses
{
    public class ExpenseType : IUserEntity<object>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        // Navigation property to access subtasks
        public ICollection<Expense>? Expense { get; set; }
    }
}
