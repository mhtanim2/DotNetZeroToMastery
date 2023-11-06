using PandaIdentity.Data;
using PandaIdentity.Interfaces;
using PandaIdentity.Models;

namespace PandaIdentity.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageRepository(DataContext context,IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFile=Path.Combine(_webHostEnvironment.ContentRootPath,
                "Images",$"{image.FileName}{image.FileExtention}");
            //Upload image in local path
            using var stream = new FileStream(localFile, FileMode.Create);
            await image.FIle.CopyToAsync(stream);

            //Url File path
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";
            image.FilePath= urlFilePath;

            //Add image to the table
            await _context.images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }
    }
}
