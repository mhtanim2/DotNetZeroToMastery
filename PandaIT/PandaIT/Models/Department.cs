using System.ComponentModel.DataAnnotations;

namespace PandaIT.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "The Department is require.")]
        public string DepartmentName { get; set; }
        // One-to-Many relationship
        public ICollection<Employee> Employee { get; set; }
    }
}
