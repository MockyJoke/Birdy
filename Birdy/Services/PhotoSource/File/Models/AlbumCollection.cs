using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Birdy.Controllers;
using Birdy.Models;
using Birdy.Util;
using Microsoft.Extensions.Logging;

namespace Birdy.Services.PhotoSource.File.Models
{
    public class AlbumCollection : IAlbumCollection
    {
        public string FilePath { get; private set; }
        public string Name { get; private set; }
        public AlbumCollection(string name, string path, string sourceId)
        {
            this.Name = name;
            this.FilePath = path;
            this.Id = new HashGenerator().SHA256(path, 6);
            this.SourceId = sourceId;
        }

        public string SourceId { get; private set; }

        public string Id { get; private set; }

        public IEnumerable<IAlbum> Albums
        {
            get
            {
                ILogger<PhotosController> logger = PhotosController.SharedLogger;
                logger.LogWarning("Enumerate Directories for: "+ FilePath);
                return Directory.EnumerateDirectories(FilePath).Select(d => new Album(this, Path.GetFileName(d)));
            }
        }
    }
}