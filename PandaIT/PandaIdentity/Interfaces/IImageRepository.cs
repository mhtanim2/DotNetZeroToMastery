using PandaIdentity.Models;

namespace PandaIdentity.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
