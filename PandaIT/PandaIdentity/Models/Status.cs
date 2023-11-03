using System.ComponentModel.DataAnnotations;

namespace PandaIdentity.Models
{
    public class Status
    {
        [Key]
        public Guid StatusId { get; set; }
        [Required(ErrorMessage = "Status Type is required.")]
        public string StatusType { get; set; }
    }
}
