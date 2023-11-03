using PandaIdentity.Models;

namespace PandaIdentity.Dto
{
    public class MyTaskDto
    {
        public Guid TaskID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        // Navigation property to access subtasks
        public ICollection<MySubTask>? MySubTask { get; set; }

    }
}
