using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Models;
using Birdy.Services.Caching;
using Birdy.Services.PhotoSource;

namespace Birdy.Services
{
    public class CachedPhotoService : IPhotoService
    {
        private IEnumerable<IPhotoSource> photoSources;
        private ICachingService<IPhoto, byte[]> cachingService;
        public CachedPhotoService(IEnumerable<IPhotoSource> photoSources, ICachingService<IPhoto, byte[]> cachingService)
        {
            this.photoSources = photoSources;
            this.cachingService = cachingService;
        }
        public IEnumerable<IAlbumCollection> AlbumCollections
        {
            get
            {
                return photoSources.SelectMany(ps => ps.AlbumCollections);
            }
        }

        public async Task<Stream> GetImageStreamAsync<T>(IPhoto photo, T param)
        {
            IPhotoSource photoSource = photoSources.Where(ps => ps.Id == photo.Parent.Parent.SourceId).FirstOrDefault();
            if (photoSource == null)
            {
                throw new ArgumentException();
            }

            byte[] imageData;
            if (await cachingService.HasKeyAsync(photo))
            {
                imageData = await cachingService.GetAsync(photo);
            }
            else
            {
                Stream imageStream = await photoSource.GetImageStreamAsync(photo);
                using (MemoryStream cacheStream = new MemoryStream())
                {
                    await imageStream.CopyToAsync(cacheStream);
                    imageData = cacheStream.ToArray();
                    await cachingService.SetAsync(photo, imageData);
                }
            }
            return new MemoryStream(imageData);
        }
    }
}