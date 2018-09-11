using System.Collections.Generic;

namespace Birdy.Models
{
    public interface IAlbumCollection
    {
        string Id { get; }
        string Name { get; }
        string SourceId { get; }
        IEnumerable<IAlbum> Albums { get; }
    }
}