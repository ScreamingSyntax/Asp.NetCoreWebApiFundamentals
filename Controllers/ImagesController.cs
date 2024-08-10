using Microsoft.AspNetCore.Mvc;
using Test.Models.Domain;
using Test.Models.DTO;
using Test.Repositories;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IIMageRepository iMageRepository;

        public ImagesController(IIMageRepository iMageRepository)
        {
            this.iMageRepository = iMageRepository;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = $"{request.FileName}_{Guid.NewGuid()}",
                    FileDescription = request.FileDescription
                };
                await iMageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] {
                ".jpg",".jpeg",".png"
            };
            if (allowedExtensions.Contains(Path.GetExtension(request.File.FileName)) == false )
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            // Not allowing more than 10 mb file
            if (request.File.Length > 1048576)
            {
                ModelState.AddModelError("file", "File size more than 10mb, please upload a smaller size file");
            }
        }
    }
}
