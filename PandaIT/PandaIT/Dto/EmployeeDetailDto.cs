using System.ComponentModel.DataAnnotations;

namespace PandaIT.Dto
{
    public class EmployeeDetailDto
    {
        public int DetailId { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}
