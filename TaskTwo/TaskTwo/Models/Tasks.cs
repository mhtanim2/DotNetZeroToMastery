using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace TaskTwo.Models
{
    public class Tasks
        {
        [Key]
        public int TaskId { get; set; }
        
        [Required(ErrorMessage = "The Title is required.")]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // One-to-many relationship
        public ICollection<SubTask> SubTasks { get; set; }
    }
}
