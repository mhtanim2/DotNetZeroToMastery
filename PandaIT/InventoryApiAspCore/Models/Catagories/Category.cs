using InventoryApiAspCore.Interfaces.Auth;
using InventoryApiAspCore.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Models.Catagories
{
    public class Category : IUserEntity<Object>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        // Navigation property to access subtasks
        public ICollection<Product>? Product { get; set; }
    }
}
