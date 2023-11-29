using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Catagories;
using InventoryApiAspCore.Models.Purchases;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Response
{
    public class ProductDto
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
        public CategoryDto Category { get; set; }
        public BrandDto Brand { get; set; }
        public ICollection<PurchaseProduct> PurchaseProduct { get; set; }
    }
}
