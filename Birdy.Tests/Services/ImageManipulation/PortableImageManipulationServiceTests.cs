using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Services.ImageManipulation;
using Birdy.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Birdy.Tests.Services.ImageManipulation
{
    [TestClass]
    public class PortableImageManipulationServiceTests
    {
        private readonly string[] IMAGE_PATHS = {
            "../../../TestData/Images/IMG_0001.JPG",
            "../../../TestData/Images/IMG_0002.JPG",
            "../../../TestData/Images/IMG_9966.JPG",
            "../../../TestData/Images/IMG_9967.JPG",
            "../../../TestData/Images/IMG_9989.JPG",
        };
        private PortableImageManipulationService imageManipulationService = new PortableImageManipulationService();
        
        [TestMethod]
        public async Task TestSingleGenerateThumbnailImageAsync()
        {
            using (EasyTimer timer = new EasyTimer("TestSingleGenerateThumbnailImageAsync"))
            {
                await doGenerateThumbnailImageAsync(IMAGE_PATHS[2]);
            }
        }

        [TestMethod]
        public async Task TestSingleGenerateHdImageAsync()
        {
            using (EasyTimer timer = new EasyTimer("TestSingleGenerateHdImageAsync"))
            {
                await doGenerateHdImageAsync(IMAGE_PATHS[2]);
            }
        }

        [TestMethod]
        public async Task TestMultipleGenerateThumbnailImageAsync()
        {
            using (EasyTimer timer = new EasyTimer("TestMultipleGenerateThumbnailImageAsync"))
            {
                foreach (string filepath in IMAGE_PATHS)
                {
                    await doGenerateThumbnailImageAsync(filepath);
                }
            }
        }

        [TestMethod]
        public async Task TestMultipleGenerateHdImageAsync()
        {
            using (EasyTimer timer = new EasyTimer("TestMultipleGenerateHdImageAsync"))
            {
                foreach (string filepath in IMAGE_PATHS)
                {
                    await doGenerateHdImageAsync(filepath);
                }
            }
        }

        [TestMethod]
        public async Task TestParallelMultipleGenerateThumbnailImageAsync()
        {
            using (EasyTimer timer = new EasyTimer("TestParallelMultipleGenerateThumbnailImageAsync"))
            {
                var taskList = IMAGE_PATHS.Select(filepath => doGenerateThumbnailImageAsync(filepath)).ToArray();
                await Task.WhenAll(taskList);
            }
        }

        [TestMethod]
        public async Task TestParallelMultipleGenerateHdImageAsync()
        {
            using (EasyTimer timer = new EasyTimer("TestParallelMultipleGenerateHdImageAsync"))
            {
                var taskList = IMAGE_PATHS.Select(filepath => doGenerateHdImageAsync(filepath)).ToArray();
                await Task.WhenAll(taskList);
            }
        }

        private async Task doGenerateThumbnailImageAsync(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            using (MemoryStream ms = new MemoryStream())
            {
                await fs.CopyToAsync(ms);
                await imageManipulationService.GenerateThumbnailImageAsync(ms.ToArray());
            }
        }

        private async Task doGenerateHdImageAsync(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            using (MemoryStream ms = new MemoryStream())
            {
                await fs.CopyToAsync(ms);
                await imageManipulationService.GenerateHdImageAsync(ms.ToArray());
            }
        }
    }
}
