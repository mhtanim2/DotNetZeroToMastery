using PandaIdentity.Models;
using System.ComponentModel.DataAnnotations;

namespace PandaIdentity.Dto
{
    public class MySubTaskCreateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? AssignedTo { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
       
    }
}
