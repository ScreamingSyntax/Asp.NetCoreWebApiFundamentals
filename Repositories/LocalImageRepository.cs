
using Test.Data;
using Test.Models.Domain;

namespace Test.Repositories
{
    public class LocalImageRepository : IIMageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TestDbContext testDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, TestDbContext testDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.testDbContext = testDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images"
                ,$"{image.FileName}{image.FileExtension}");
            // Reads the file stream, for the local file path and it knows that it has to create
            using var stream= new FileStream(localFilePath, FileMode.Create);
            //uploaded locally
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/images/image.jpg FOr this path we can write the commented code below but I prefer adding path only without local host
         //  var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/images/{image.FileName}";
            var urlFilePath = $"/images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;   
            await testDbContext.AddAsync(image);
            await testDbContext.SaveChangesAsync();
            return image;   
        }
    }
}
