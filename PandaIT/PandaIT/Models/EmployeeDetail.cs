using System.ComponentModel.DataAnnotations;

namespace PandaIT.Models
{
    public class EmployeeDetail
    {
        [Key]
        public int DetailId { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "The product Contact Number is required.")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "The product Email is required.")]
        public string Email { get; set; }
    }
}
