using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaIdentity.Dto
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile FIle { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        //public string FileExtention { get; set; }
        //public long FileSizeInBytes { get; set; }
        //public string FilePath { get; set; }
    }
}
