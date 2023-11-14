using InventoryApiAspCore.Models.Brands;
using InventoryApiAspCore.Models.Catagories;
using InventoryApiAspCore.Models.Purchases;

namespace InventoryApiAspCore.Models.Products
{
    public class Product
    {
        public Guid Id { get; set; }
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
