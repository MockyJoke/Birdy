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
    public class ImagesController : ControllerBase
    {
        private const string IMAGE_CONTENT_TYPE = "image/jpeg";
        private IPhotoService imageService;
        private PhotoExtractor photoExtractor;

        public ImagesController(IPhotoService imageService, PhotoExtractor photoExtractor)
        {
            this.imageService = imageService;
            this.photoExtractor = photoExtractor;
        }

        // GET api/Images/{albumCollectionId}/{AlbumId}/{photoId}
        [HttpGet("{albumCollectionId}/{AlbumId}/{photoId}")]
        public async Task<ActionResult> Get(string albumCollectionId, string albumId, string photoId)
        {
            IPhoto photo;
            try
            {
                photo = photoExtractor.Single(imageService.AlbumCollections, albumCollectionId, albumId, photoId);
                Stream imageStream = await imageService.GetImageStreamAsync<string>(photo, null);
                return File(imageStream, IMAGE_CONTENT_TYPE);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
