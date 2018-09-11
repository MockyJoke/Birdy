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
    public class AlbumCollectionsController : ControllerBase
    {
        private IPhotoService imageService;
        private AlbumCollectionExtractor albumCollectionExtractor;

        public AlbumCollectionsController(IPhotoService imageService, AlbumCollectionExtractor albumCollectionExtractor)
        {
            this.imageService = imageService;
            this.albumCollectionExtractor = albumCollectionExtractor;
        }
        // GET api/values
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

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            IAlbumCollection albumCollection;
            try
            {
                albumCollection = albumCollectionExtractor.Single(imageService.AlbumCollections, id);
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
    }
}
