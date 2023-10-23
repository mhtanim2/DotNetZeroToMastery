using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PandaIT.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Many to One relation
        [JsonIgnore]
        public Department Department { get; set; }
        // One-to-One relationship
        public EmployeeDetail EmployeeDetail { get; set; }
        // Many-to-Many relationship
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
