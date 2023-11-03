using PandaIdentity.Models;
using System.ComponentModel.DataAnnotations;

namespace PandaIdentity.Dto
{
    public class MySubTaskDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public MyTask MyTask { get; set; }

    }
}
