using InventoryApiAspCore.Models.Suppliers;
using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Request
{
    public class PurchaseRequestDto
    {
        
        public int VatTax { get; set; }

        public int? Discount { get; set; }

        public int? OtherCost { get; set; }

        public int? ShippingCost { get; set; }

        public int GrandTotal { get; set; }

        public string Note { get; set; }

        public ICollection<PurchaseProductRequestDto> PurchaseProductRequestDto { get; set; }
    }

}
