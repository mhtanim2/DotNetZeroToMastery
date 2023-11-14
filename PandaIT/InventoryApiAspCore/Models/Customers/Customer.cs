using InventoryApiAspCore.Interfaces.Auth;
using InventoryApiAspCore.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Models.Customers
{
    public class Customer : IUserEntity<object>
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigate the child
        public ICollection<Product>? Product { get; set; }

    }
}
