using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaskOne.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String? Tasks { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
