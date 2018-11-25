using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Models;
using Birdy.Services.Caching;
using Birdy.Services.ImageManipulation;
using Birdy.Services.PhotoSource;

namespace Birdy.Services
{
    public class CachedPhotoService : IPhotoService
    {
        private IEnumerable<IPhotoSource> photoSources;
        private ICachingService<string, byte[]> cachingService;
        private IImageManipulationService imageManipulationService;
        public CachedPhotoService(IEnumerable<IPhotoSource> photoSources,
            ICachingService<string, byte[]> cachingService,
            IImageManipulationService imageManipulationService)
        {
            this.photoSources = photoSources;
            this.cachingService = cachingService;
            this.imageManipulationService = imageManipulationService;
        }
        public IEnumerable<IAlbumCollection> AlbumCollections
        {
            get
            {
                return photoSources.SelectMany(ps => ps.AlbumCollections);
            }
        }

        public async Task<Stream> GetImageStreamAsync(IPhoto photo, ImageFetchMode mode)
        {
            IPhotoSource photoSource = photoSources.Where(ps => ps.Id == photo.Parent.Parent.SourceId).FirstOrDefault();
            if (photoSource == null)
            {
                throw new ArgumentException();
            }
            byte[] imageData;
            switch (mode)
            {
                case ImageFetchMode.FULL:
                    imageData = await GetCacheableFullImageAsync(photo, photoSource);
                    break;
                case ImageFetchMode.HD:
                    imageData = await GetCacheableHdImageAsync(photo, photoSource);
                    break;
                case ImageFetchMode.MINI:
                    imageData = await GetCacheableThumbnailImageAsync(photo, photoSource);
                    break;
                default:
                    imageData = await GetCacheableHdImageAsync(photo, photoSource);
                    break;
            }
            return new MemoryStream(imageData);
        }

        private async Task<byte[]> GetCacheableFullImageAsync(IPhoto photo, IPhotoSource photoSource)
        {
            byte[] imageData;
            if (await cachingService.HasKeyAsync(photo.GetHashCode().ToString()))
            {
                imageData = await cachingService.GetAsync(photo.GetHashCode().ToString());
            }
            else
            {
                Stream imageStream = await photoSource.GetImageStreamAsync(photo);
                using (MemoryStream cacheStream = new MemoryStream())
                {
                    await imageStream.CopyToAsync(cacheStream);
                    imageData = cacheStream.ToArray();
                }
            }
            return imageData;
        }

        private async Task<byte[]> GetCacheableHdImageAsync(IPhoto photo, IPhotoSource photoSource)
        {
            byte[] imageData;
            string cacheKey = $"{photo.GetHashCode().ToString()}_Hd";
            if (await cachingService.HasKeyAsync(cacheKey))
            {
                imageData = await cachingService.GetAsync(cacheKey);
            }
            else
            {
                byte[] fullImageData = await GetCacheableFullImageAsync(photo, photoSource);
                imageData = await imageManipulationService.GenerateHdImageAsync(fullImageData);
                await cachingService.SetAsync(cacheKey, imageData);
            }
            return imageData;
        }

        private async Task<byte[]> GetCacheableThumbnailImageAsync(IPhoto photo, IPhotoSource photoSource)
        {
            byte[] imageData;
            string cacheKey = $"{photo.GetHashCode().ToString()}_Thumbnail";
            if (await cachingService.HasKeyAsync(cacheKey))
            {
                imageData = await cachingService.GetAsync(cacheKey);
            }
            else
            {
                byte[] fullImageData = await GetCacheableFullImageAsync(photo, photoSource);
                imageData = await imageManipulationService.GenerateThumbnailImageAsync(fullImageData);
                await cachingService.SetAsync(cacheKey, imageData);
            }
            return imageData;
        }
    }
}