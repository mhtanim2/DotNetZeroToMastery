using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Response
{
    public class SupplierDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
