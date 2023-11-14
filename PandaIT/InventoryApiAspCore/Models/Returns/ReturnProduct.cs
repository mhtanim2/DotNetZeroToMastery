using InventoryApiAspCore.Models.Products;
using InventoryApiAspCore.Models.Purchases;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Models.Returns
{
    public class ReturnProduct
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
        public Return Return { get; set; }
        public Product Product { get; set; }
    }
}
