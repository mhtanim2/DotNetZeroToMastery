using System.ComponentModel.DataAnnotations;

namespace PandaIdentity.Models
{
    public class MyTask
    {
        [Key]
        public Guid TaskID { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        // Navigation property to access subtasks
        public ICollection<MySubTask>? MySubTask { get; set; }
    }
}
