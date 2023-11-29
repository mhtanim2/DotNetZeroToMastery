using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Request
{
    public class PurchaseProductRequestDto
    {
        
        public int Qty { get; set; }

        public int UnitCost { get; set; }

        public int Total { get; set; }
        
    }
}
