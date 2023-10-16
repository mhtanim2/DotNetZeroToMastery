using System.ComponentModel.DataAnnotations;

namespace TaskTwo.Dto
{
    public class SubTaskDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        //FK
        public int TaskId { get; set; }
    }
}
