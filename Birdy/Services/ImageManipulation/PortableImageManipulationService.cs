using System;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Birdy.Services.ImageManipulation
{
    public class PortableImageManipulationService : IImageManipulationService
    {
        public Task<byte[]> GenerateHdImageAsync(Stream imageStream)
        {
            MemoryStream outputStream = new MemoryStream();
            using (Image<Rgba32> image = Image.Load(imageStream))
            {
                Tuple<int, int> outputResolution = calculateResolution(new Tuple<int, int>(image.Width, image.Height), 1280);
                image.Mutate(ctx => ctx.Resize(outputResolution.Item1, outputResolution.Item2));
                image.SaveAsJpeg(outputStream);
            }

            return Task.FromResult(outputStream.ToArray());
        }

        public Task<byte[]> GenerateThumbnailImageAsync(Stream imageStream)
        {
            MemoryStream outputStream = new MemoryStream();
            using (Image<Rgba32> image = Image.Load(imageStream))
            {
                Tuple<int, int> outputResolution = calculateResolution(new Tuple<int, int>(image.Width, image.Height), 320);
                image.Mutate(ctx => ctx.Resize(outputResolution.Item1, outputResolution.Item2));
                image.SaveAsJpeg(outputStream);
            }
            return Task.FromResult(outputStream.ToArray());
        }

        private Tuple<int, int> calculateResolution(Tuple<int, int> input, int target)
        {
            if (input.Item1 > input.Item2)
            {
                double scaleFactor = input.Item2 / (double)target;
                return new Tuple<int, int>(Convert.ToInt32(input.Item1 / scaleFactor), target);
            }
            else
            {
                double scaleFactor = input.Item1 / (double)target;
                return new Tuple<int, int>(target, Convert.ToInt32(input.Item2 / scaleFactor));
            }
        }
    }
}