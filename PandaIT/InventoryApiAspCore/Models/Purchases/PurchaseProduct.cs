using InventoryApiAspCore.Models.Products;
using InventoryApiAspCore.Models.Suppliers;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryApiAspCore.Models.Purchases
{
    public class PurchaseProduct
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public int Qty { get; set; }
        public int UnitCost { get; set; }
        public int Total { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        // Every purchase has to have a supplier 
        public Purchase Purchase { get; set; }
        public Product Product { get; set; }
    }
}
