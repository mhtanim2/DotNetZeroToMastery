using InventoryApiAspCore.Models.Suppliers;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryApiAspCore.Models.Purchases
{
    public class Purchase
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public int VatTax { get; set; }
        public int? Discount { get; set; }
        public int? OtherCost { get; set; }
        public int? ShippingCost { get; set; }
        public int GrandTotal { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        // Every purchase has to have a supplier 
        public Supplier Supplier { get; set; }
    }
}
