using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models.Domain
{
    public class Image
    {
        public int Id { get; set; }

        [NotMapped]
        public IFormFile File{ get; set; }

        public String FileName { get; set; }

        public String? FileDescription { get; set; }

        public String FileExtension { get; set; }

        public long FileSizeInBytes {  get; set; }

        public string FilePath { get; set; }
    }
}
