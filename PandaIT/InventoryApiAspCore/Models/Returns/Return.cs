using InventoryApiAspCore.Models.Customers;
using InventoryApiAspCore.Models.Suppliers;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Models.Returns
{
    public class Return
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
        public Customer Customer { get; set; }
    }
}
