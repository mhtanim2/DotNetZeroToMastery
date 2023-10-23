namespace PandaIT.Models
{
    public class EmployeeProject
    {
        //Fk
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        //FK
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
