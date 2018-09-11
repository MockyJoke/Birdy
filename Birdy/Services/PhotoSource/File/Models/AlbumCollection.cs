using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Birdy.Models;
using Birdy.Util;

namespace Birdy.Services.PhotoSource.File.Models
{
    public class AlbumCollection : IAlbumCollection
    {
        public string FilePath { get; private set; }
        public AlbumCollection(string path, string sourceId)
        {

            this.FilePath = path;
            this.Id = new HashGenerator().SHA256(path, 6);
            this.SourceId = sourceId;
        }

        public string SourceId { get; private set; }

        public string Id { get; private set; }

        public string Name
        {
            get
            {
                return FilePath;
            }
        }
        public IEnumerable<IAlbum> Albums
        {
            get
            {
                return Directory.EnumerateDirectories(FilePath).Select(d => new Album(this, Path.GetFileName(d)));
            }
        }
    }
}