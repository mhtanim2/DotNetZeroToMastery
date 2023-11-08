using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PandaIdentity.Models
{
    public class MySubTask
    {
        [Key]
        public Guid SubTaskID { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
        public string? AssignedTo { get; set; }
        // Nevigate the staus 1 to 1
        [Required(ErrorMessage = "Status is required.")]
        public Status Status { get; set; }
        // Navigate The Priority 1 to 1
        [Required(ErrorMessage = "Priority is required.")]
        public Priority Priority { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        
        //Parents Direction
        [JsonIgnore]
        public MyTask MyTask { get; set; }
    
    }
}
