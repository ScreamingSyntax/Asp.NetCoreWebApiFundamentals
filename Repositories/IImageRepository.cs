using Test.Models.Domain;

namespace Test.Repositories
{
    public interface IIMageRepository
    {
        Task<Image> Upload(Image image);

    }
}
