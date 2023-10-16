using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskTwo.Models
{
    public class SubTask
    {
        [Key]
        public int SubTaskId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        // Fk
        public int TaskId { get; set; }
        [JsonIgnore]
        public Tasks Task { get; set; }
    }
}
