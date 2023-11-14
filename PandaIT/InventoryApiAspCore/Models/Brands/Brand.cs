using InventoryApiAspCore.Interfaces.Auth;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Models.Brands
{
    public class Brand :IUserEntity<Object>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
