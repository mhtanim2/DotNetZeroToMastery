using System.ComponentModel.DataAnnotations;

namespace PandaIT.Dto
{
    public class ProjectDto
    {

        public int ProjectId { get; set; }
        [Required(ErrorMessage = "The product Project Number is required.")]
        public string ProjectName { get; set; }
    }
}
