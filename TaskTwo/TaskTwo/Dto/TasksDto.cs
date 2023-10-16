using System.ComponentModel.DataAnnotations;
using TaskTwo.Models;

namespace TaskTwo.Dto
{
    public class TasksDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        // One-to-many relationship
        public ICollection<SubTaskDto> SubTaskDto { get; set; }
    }
}
