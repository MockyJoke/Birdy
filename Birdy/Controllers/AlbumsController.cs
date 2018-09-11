using System;
using System.Collections.Generic;
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
    public class AlbumsController : ControllerBase
    {
        private IPhotoService imageService;
        private AlbumExtractor albumExtractor;

        public AlbumsController(IPhotoService imageService, AlbumExtractor albumExtractor)
        {
            this.imageService = imageService;
            this.albumExtractor = albumExtractor;
        }

        // GET api/values/5
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
    }
}
