using System.Collections.Generic;
using System.Linq;
using Birdy.Models;

namespace Birdy.Util.Extraction
{
    public class AlbumExtractor
    {
        private AlbumCollectionExtractor albumCollectionExtractor;

        public AlbumExtractor(AlbumCollectionExtractor albumCollectionExtractor)
        {
            this.albumCollectionExtractor = albumCollectionExtractor;
        }

        public IAlbum Single(IEnumerable<IAlbumCollection> albumCollections, string albumCollectionId, string albumId)
        {
            IAlbumCollection albumCollection = albumCollectionExtractor.Single(albumCollections, albumCollectionId);
            return albumCollection.Albums.Where(a => a.Id == albumId).First();
        }
    }
}