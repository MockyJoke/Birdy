using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Models;
using Birdy.Services;
using Birdy.Util.Extension;
using Birdy.Util.Extraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Birdy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private const string IMAGE_CONTENT_TYPE = "image/jpeg";
        private const int MAX_ALBUM_PREVIEW_SAMPLES = 4;
        private IPhotoService imageService;
        private AlbumCollectionExtractor albumCollectionExtractor;
        private AlbumExtractor albumExtractor;
        private PhotoExtractor photoExtractor;
        public static ILogger<PhotosController> SharedLogger;

        public PhotosController(
            IPhotoService imageService,
            AlbumCollectionExtractor albumCollectionExtractor,
            AlbumExtractor albumExtractor,
            PhotoExtractor photoExtractor,
            ILogger<PhotosController> logger 
            )
        {
            this.imageService = imageService;
            this.albumCollectionExtractor = albumCollectionExtractor;
            this.albumExtractor = albumExtractor;
            this.photoExtractor = photoExtractor;
            SharedLogger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var albumCollections = imageService.AlbumCollections.Select(ac => new
            {
                id = ac.Id,
                name = ac.Name
            });
            return new JsonResult(albumCollections);
        }

        [HttpGet("{albumCollectionId}")]
        public ActionResult Get(string albumCollectionId)
        {
            IAlbumCollection albumCollection;
            try
            {
                SharedLogger.LogWarning("Error occured during AlbumCollection extraction.");
                albumCollection = albumCollectionExtractor.Single(imageService.AlbumCollections, albumCollectionId);
            }
            catch(Exception ex)
            {
                SharedLogger.LogError(ex,"Error occured during AlbumCollection extraction.");
                return NotFound();
            }
            return new JsonResult(new
            {
                id = albumCollection.Id,
                name = albumCollection.Name,
                albums = albumCollection.Albums.Select(a => new { id = a.Id, name = a.Name })
            });
        }

        [HttpGet("{albumCollectionId}/{AlbumId}")]
        public async Task<ActionResult> Get(string albumCollectionId, string albumId, bool preview)
        {
            IAlbum album;
            IAlbumCollection albumCollection;

            List<string> previewImages = null;
            try
            {
                albumCollection = albumCollectionExtractor.Single(imageService.AlbumCollections, albumCollectionId);
                album = albumExtractor.Single(imageService.AlbumCollections, albumCollectionId, albumId);
                if (preview)
                {
                    IEnumerable<IPhoto> photos = album.Photos.Take(MAX_ALBUM_PREVIEW_SAMPLES);
                    Stream[] previewPhotoStreams = await Task.WhenAll(photos.Select(p => imageService.GetImageStreamAsync(p, ImageFetchMode.MINI)));
                    previewImages = previewPhotoStreams.Select(s => s.ConvertToBase64()).ToList();
                }
            }
            catch(Exception ex)
            {
                SharedLogger.LogError(ex,"Error occured When getting Album");
                return NotFound();
            }

            return new JsonResult(new
            {
                albumCollectionId = albumCollection.Id,
                albumCollectionName = albumCollection.Name,
                id = album.Id,
                name = album.Name,
                photos = album.Photos.Select(p => new { id = p.Id, name = p.Name }),
                previewImages = previewImages
            });
        }

        [HttpGet("{albumCollectionId}/{AlbumId}/{photoId}/{mode?}")]
        public async Task<ActionResult> Get(string albumCollectionId, string albumId, string photoId, string mode)
        {
            IAlbum album;
            IAlbumCollection albumCollection;
            IPhoto photo;
            try
            {
                albumCollection = albumCollectionExtractor.Single(imageService.AlbumCollections, albumCollectionId);
                album = albumExtractor.Single(imageService.AlbumCollections, albumCollectionId, albumId);
                photo = photoExtractor.Single(imageService.AlbumCollections, albumCollectionId, albumId, photoId);
                Stream imageStream;
                if (mode == "full")
                {
                    imageStream = await imageService.GetImageStreamAsync(photo, ImageFetchMode.FULL);
                }
                else if (mode == "mini")
                {
                    imageStream = await imageService.GetImageStreamAsync(photo, ImageFetchMode.MINI);
                }
                else if (mode == "hd")
                {
                    imageStream = await imageService.GetImageStreamAsync(photo, ImageFetchMode.HD);
                }
                else
                {
                    return new JsonResult(new
                    {
                        albumCollectionId = albumCollection.Id,
                        albumCollectionName = albumCollection.Name,
                        albumId = album.Id,
                        albumName = album.Name,
                        id = photo.Id,
                        name = photo.Name
                    });
                }
                return File(imageStream, IMAGE_CONTENT_TYPE);
            }
            catch(Exception ex)
            {
                SharedLogger.LogError(ex,"Error occured when getting Photo");
                return new JsonResult(ex.ToString());
            }
        }
    }
}
