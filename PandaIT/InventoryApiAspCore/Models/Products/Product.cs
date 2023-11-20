using InventoryApiAspCore.Interfaces.Auth;
using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Catagories;
using InventoryApiAspCore.Models.Purchases;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Models.Products
{
    public class Product: IUserEntity<object>
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string? Details { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        // FK
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public ICollection<PurchaseProduct> PurchaseProduct { get; set; }
    }
}
