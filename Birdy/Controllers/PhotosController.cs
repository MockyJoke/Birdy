using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Birdy.Models;
using Birdy.Services;
using Birdy.Util.Extraction;
using Microsoft.AspNetCore.Mvc;

namespace Birdy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private const string IMAGE_CONTENT_TYPE = "image/jpeg";
        private IPhotoService imageService;
        private AlbumCollectionExtractor albumCollectionExtractor;
        private AlbumExtractor albumExtractor;
        private PhotoExtractor photoExtractor;

        public PhotosController(
            IPhotoService imageService,
            AlbumCollectionExtractor albumCollectionExtractor,
            AlbumExtractor albumExtractor,
            PhotoExtractor photoExtractor
            )
        {
            this.imageService = imageService;
            this.albumCollectionExtractor = albumCollectionExtractor;
            this.albumExtractor = albumExtractor;
            this.photoExtractor = photoExtractor;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var albumCollections = imageService.AlbumCollections.Select(ac => new
            {
                ac.Id,
                ac.Name
            });
            return new JsonResult(albumCollections);
        }

        [HttpGet("{albumCollectionId}")]
        public ActionResult Get(string albumCollectionId)
        {
            IAlbumCollection albumCollection;
            try
            {
                albumCollection = albumCollectionExtractor.Single(imageService.AlbumCollections, albumCollectionId);
            }
            catch
            {
                return NotFound();
            }
            return new JsonResult(new
            {
                id = albumCollection.Id,
                name = albumCollection.Name,
                albumsIds = albumCollection.Albums.Select(a => a.Id)
            });
        }

        [HttpGet("{albumCollectionId}/{AlbumId}")]
        public ActionResult Get(string albumCollectionId, string albumId)
        {
            IAlbum album;
            try
            {
                album = albumExtractor.Single(imageService.AlbumCollections, albumCollectionId, albumId);
            }
            catch
            {
                return NotFound();
            }

            return new JsonResult(new
            {
                id = album.Id,
                name = album.Name,
                photoIds = album.Photos.Select(p => p.Id)
            });
        }

        [HttpGet("{albumCollectionId}/{AlbumId}/{photoId}/{mode?}")]
        public async Task<ActionResult> Get(string albumCollectionId, string albumId, string photoId, string mode)
        {
            IPhoto photo;
            try
            {
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
                else if (mode == "meta")
                {
                    return new JsonResult(new { photo.Id, photo.Name });
                }
                else
                {
                    imageStream = await imageService.GetImageStreamAsync(photo, ImageFetchMode.HD);
                }
                return File(imageStream, IMAGE_CONTENT_TYPE);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
