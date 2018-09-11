using System.Collections.Generic;

namespace Birdy.Models
{
    public interface IAlbum
    {
        string Id { get; }
        string Name { get; }
        IAlbumCollection Parent { get; }
        IEnumerable<IPhoto> Photos { get; }
    }
}