using System;
using System.IO;
using System.Threading.Tasks;
using SkiaSharp;

namespace Birdy.Services.ImageManipulation
{
    public class SkiaImageManipulationService : IImageManipulationService
    {
        public Task<byte[]> GenerateHdImageAsync(byte[] imageData)
        {
            return Task.Run(() =>
            {
                return resize(imageData, 1280);
            });
        }

        public Task<byte[]> GenerateThumbnailImageAsync(byte[] imageData)
        {
            return Task.Run(() =>
            {
                return resize(imageData, 160);
            });
        }

        private byte[] resize(byte[] imageData, int size)
        {
            using (var inputStream = new SKManagedStream(new MemoryStream(imageData)))
            {
                using (var original = SKBitmap.Decode(inputStream))
                {
                    int width, height;
                    if (original.Width > original.Height)
                    {
                        width = size;
                        height = original.Height * size / original.Width;
                    }
                    else
                    {
                        width = original.Width * size / original.Height;
                        height = size;
                    }

                    using (var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.High))
                    {
                        if (resized == null) return null;
                        using (var image = SKImage.FromBitmap(resized))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                image.Encode(SKEncodedImageFormat.Jpeg, 90).SaveTo(ms);
                                return ms.ToArray();
                            }
                        }
                    }
                }
            }
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