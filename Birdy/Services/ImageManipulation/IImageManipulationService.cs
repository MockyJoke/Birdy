using System.IO;
using System.Threading.Tasks;

namespace Birdy.Services.ImageManipulation
{
    public interface IImageManipulationService{
        Task<byte[]> GenerateHdImageAsync(byte[] imageData);
        Task<byte[]> GenerateThumbnailImageAsync(byte[] imageData);
    }
}