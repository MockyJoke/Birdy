using System.Collections.Generic;
using System.Linq;
using Birdy.Models;

namespace Birdy.Util.Extraction
{
    public class AlbumCollectionExtractor
    {
        public IAlbumCollection Single(IEnumerable<IAlbumCollection> albumCollections, string id)
        {
            return albumCollections.Where(ac => ac.Id == id).First();
        }
    }
}