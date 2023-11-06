using System.ComponentModel.DataAnnotations.Schema;

namespace PandaIdentity.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile FIle { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtention { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
