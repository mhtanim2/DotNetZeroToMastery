using System.ComponentModel.DataAnnotations;
namespace PandaIT.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "The product Project Number is required.")]
        public string ProjectName { get; set; }
        // Many-to-Many relationship
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
