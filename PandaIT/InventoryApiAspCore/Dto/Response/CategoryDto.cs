using System.ComponentModel.DataAnnotations;

namespace InventoryApiAspCore.Dto.Response
{
    public class CategoryDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
