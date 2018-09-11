using System;

namespace Birdy.Models
{
    public interface IPhoto : IComparable<IPhoto>
    {
        string Id { get; }
        string Name { get; }
        IAlbum Parent { get; }
    }
}