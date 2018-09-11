using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Birdy.Models;

namespace Birdy.Services.PhotoSource
{

    public interface IPhotoSource
    {
        string Id { get; }
        IEnumerable<IAlbumCollection> AlbumCollections { get; }
        Task<Stream> GetImageStreamAsync(IPhoto photo);
    }
}