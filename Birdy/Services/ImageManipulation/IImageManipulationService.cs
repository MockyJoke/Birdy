using System.IO;
using System.Threading.Tasks;

namespace Birdy.Services.ImageManipulation
{
    public interface IImageManipulationService{
        Task<byte[]> GenerateHdImageAsync(Stream imageStream);
        Task<byte[]> GenerateThumbnailImageAsync(Stream imageStream);
    }
}