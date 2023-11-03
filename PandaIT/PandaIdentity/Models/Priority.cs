using System.ComponentModel.DataAnnotations;

namespace PandaIdentity.Models
{
    public class Priority
    {
        [Key]
        public Guid PriorityId { get; set; }
        [Required(ErrorMessage = "Priority Type is required.")]
        public string PriorityType { get; set; }
    }
}