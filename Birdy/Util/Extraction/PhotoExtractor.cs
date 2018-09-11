using System.Collections.Generic;
using System.Linq;
using Birdy.Models;

namespace Birdy.Util.Extraction
{
    public class PhotoExtractor
    {
        private AlbumExtractor albumExtractor;

        public PhotoExtractor(AlbumExtractor albumExtractor)
        {
            this.albumExtractor = albumExtractor;
        }

        public IPhoto Single(IEnumerable<IAlbumCollection> albumCollections, string albumCollectionId, string albumId, string photoId)
        {
            IAlbum album = albumExtractor.Single(albumCollections, albumCollectionId, albumId);
            return album.Photos.Where(p => p.Id == photoId).First();
        }
    }
}