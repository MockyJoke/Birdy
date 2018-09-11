using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Birdy.Models;

namespace Birdy.Services
{
    public interface IPhotoService
    {
        IEnumerable<IAlbumCollection> AlbumCollections { get; }
        Task<Stream> GetImageStreamAsync<T>(IPhoto photo, T param);
    }
}
