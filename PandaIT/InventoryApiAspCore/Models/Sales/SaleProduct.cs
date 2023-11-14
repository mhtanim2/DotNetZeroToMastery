using InventoryApiAspCore.Models.Products;
using InventoryApiAspCore.Models.Purchases;
using InventoryApiAspCore.Models.Returns;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Models.Sales
{
    public class SaleProduct
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
        public Sale Sale { get; set; }
        public Product Product { get; set; }

    }
}
