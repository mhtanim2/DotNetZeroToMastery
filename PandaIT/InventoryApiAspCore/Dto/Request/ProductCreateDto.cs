using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Catagories;
using InventoryApiAspCore.Models.Purchases;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Request
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
//        [Required]
//        public string UserEmail { get; set; }

        public string Unit { get; set; }
        public string? Details { get; set; }
        // FK
//        public Category Category { get; set; }
//        public Brand Brand { get; set; }
    }
}