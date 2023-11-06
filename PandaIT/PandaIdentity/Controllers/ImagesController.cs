using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaIdentity.Dto;
using PandaIdentity.Interfaces;
using PandaIdentity.Models;

namespace PandaIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        // Post; /api/images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto) 
        {
            ValidateFileUpload(imageUploadRequestDto);
            if (ModelState.IsValid)
            {// Repository only work with Domain model 
                Image ob = new Image();
                ob.FIle=imageUploadRequestDto.FIle;
                ob.FileName=imageUploadRequestDto.FileName;
                ob.FileExtention = Path.GetExtension(imageUploadRequestDto.FIle.FileName);
                ob.FileSizeInBytes = imageUploadRequestDto.FIle.Length;
                ob.FileDescription = imageUploadRequestDto.FileDescription;

                //Use repository to upload the iamge file
                await _imageRepository.Upload(ob);
                return Ok(ob);
            }

            return BadRequest(ModelState);
        }

        //Check the image uploading validation
        private void ValidateFileUpload(ImageUploadRequestDto requestDto) 
        {
            var allowedExtention = new String[] {".jpg", ".jpeg",".png" };
            var extaintion = Path.GetExtension(requestDto.FIle.FileName);
            if (!allowedExtention.Contains(extaintion))
            {
                ModelState.AddModelError("file","Upload only jpg, jpeg and png files ");
            }
            if (requestDto.FIle.Length>10485760)
            {
                ModelState.AddModelError("file","Failed, File size more than 10mb");
            }
        }
    }
}
